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
                        { "planted_counter", 0 },
                        { "upgrade_plant_counter", 0 },
                        { "full_upgrade_plant_counter", 0 },
                        { "collect_resource_counter", 0 },
                        { "unlocked_achievements", 0 },
                        { "sacrifice_counter", 0 },
                        { "planted_plants_counter", 0 },
                        { "level_up_counter", 0 },
                        { "highest_score", 0 },
                        { "complete_tutorial", false },
                        { "survival_data", new List<bool> { false, false, false, false, false } },
                        { "active_plant_data", new List<bool> { false, false } },
                        { "explosive_data", new List<bool> { false } },
                        { "planted_in_time_data", new List<bool> { false, false } },
                        { "utils_data", new List<bool> { false, false, false, false, false, false, false } }
                    };

                    docRef.SetAsync(data).ContinueWithOnMainThread(task =>
                    {
                        if (task.IsCompleted)
                        {
                            PlayerManager.Instance.Die = 0;
                            PlayerManager.Instance.Kill = 0;
                            PlayerManager.Instance.Planted = 0;
                            PlayerManager.Instance.UpgradePlantCounter = 0;
                            PlayerManager.Instance.FullUpgradePlantCounter = 0;
                            PlayerManager.Instance.CollectResourceCounter = 0;
                            PlayerManager.Instance.UnlockedAchievements = 0;
                            PlayerManager.Instance.SacrificeCounter = 0;
                            PlayerManager.Instance.PlantedPlants = 0;
                            PlayerManager.Instance.LevelUpCounter = 0;
                            PlayerManager.Instance.SurvivalData = new List<bool> { false, false, false, false, false, false };
                            PlayerManager.Instance.ActivePlantData = new List<bool> { false, false };
                            PlayerManager.Instance.ExplosiveData = new List<bool> { false };
                            PlayerManager.Instance.PlantedInTimeData = new List<bool> { false, false };
                            PlayerManager.Instance.UtilsData = new List<bool> { false, false, false, false, false, false, false };
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
