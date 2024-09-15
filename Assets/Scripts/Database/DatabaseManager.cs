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

                OnFirebaseInitialized?.Invoke();  
                
                initializeUserData();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + task.Result);
            }
        });
    }


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
                        { "explode_counter", 0 },
                        { "resource_counter", 0 },
                        { "upgrade_counter", 0 },
                        { "level_up_counter", 0 },
                        { "highest_score", 0 },
                        { "max_upgrade", 0 },
                        { "complete_tutorial", false }
                    };

                    docRef.SetAsync(data).ContinueWithOnMainThread(task =>
                    {
                        if (task.IsCompleted)
                        {
                            PlayerManager.Instance.Die = 0;
                            PlayerManager.Instance.Kill = 0;
                            PlayerManager.Instance.Planted = 0;
                            PlayerManager.Instance.UpgradePlantCounter = 0;
                            PlayerManager.Instance.FullyUpgrade = 0;
                        }
                    });
                }
            });
        }
        catch (Exception ex)
        {
            Debug.LogError("Error getting or setting user data: " + ex.Message);
        }
    }
}
