using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Extensions;
using Firebase.Firestore;

public class DatabaseManager : MonoBehaviour
{
    // Singleton instance
    public static DatabaseManager Instance { get; private set; }

    public FirebaseFirestore Db { get; private set; }
    private bool isInitialized = false;

    public event Action OnFirebaseInitialized; // Event to signal when Firebase is ready

    void Awake()
    {
        // Ensure there's only one instance of DatabaseManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeFirebase();  // Start Firebase initialization
    }

    private void InitializeFirebase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                FirebaseApp app = FirebaseApp.DefaultInstance;
                Db = FirebaseFirestore.DefaultInstance;
                isInitialized = true;

                OnFirebaseInitialized?.Invoke();  // Notify listeners Firebase is ready
                
                // Now that Firebase is initialized, initialize the user data
                initializeUserData();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + task.Result);
            }
        });
    }

    // Method to check if Firebase is initialized
    public bool IsInitialized()
    {
        return isInitialized;
    }

    private void initializeUserData()
    {
        if (!isInitialized)
        {
            Debug.LogError("Firebase is not initialized yet.");
            return;
        }

        string hostname = System.Environment.MachineName;
        DocumentReference docRef = Db.Collection("users").Document(hostname);

        try
        {
            docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                DocumentSnapshot snapshot = task.Result;

                if (!snapshot.Exists)
                {

                    Dictionary<string, object> data = new Dictionary<string, object>
                    {
                        { "die_counter", 0 },
                        { "kill_counter", 0 },
                        { "plant_counter", 0 },
                        { "upgrade_counter", 0 },
                        { "highest_score", 0 },
                        { "max_upgrade", 0 },
                        { "complete_tutorial", false }
                    };

                    docRef.SetAsync(data).ContinueWithOnMainThread(task =>
                    {
                        if (task.IsCompleted)
                        {
                            // Initialize player data with default values
                            SingletonGame.Instance.PlayerManager.Die = 0;
                            SingletonGame.Instance.PlayerManager.Kill = 0;
                            SingletonGame.Instance.PlayerManager.Planted = 0;
                            SingletonGame.Instance.PlayerManager.Upgraded = 0;
                            SingletonGame.Instance.PlayerManager.FullyUpgrade = 0;
                        }
                        else
                        {
                            Debug.LogError("Failed to write user data: " + task.Exception);
                        }
                    });
                }
                else
                {
                    // If user data exists, retrieve it using snapshot.ToDictionary()
                    Dictionary<string, object> documentData = snapshot.ToDictionary();

                    // Retrieve counters
                    SingletonGame.Instance.PlayerManager.Die = Convert.ToInt32(documentData.GetValueOrDefault("die_counter", 0));
                    SingletonGame.Instance.PlayerManager.Kill = Convert.ToInt32(documentData.GetValueOrDefault("kill_counter", 0));
                    SingletonGame.Instance.PlayerManager.Planted = Convert.ToInt32(documentData.GetValueOrDefault("plant_counter", 0));
                    SingletonGame.Instance.PlayerManager.Upgraded = Convert.ToInt32(documentData.GetValueOrDefault("upgrade_counter", 0));
                    SingletonGame.Instance.PlayerManager.FullyUpgrade = Convert.ToInt32(documentData.GetValueOrDefault("max_upgrade", 0));
                }
            });
        }
        catch (Exception ex)
        {
            Debug.LogError("Error getting or setting user data: " + ex.Message);
        }
    }
}
