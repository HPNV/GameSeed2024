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

    void Awake()
    {
        // Check if there is already an instance of DatabaseManager
        if (Instance == null)
        {
            // If not, set it to this instance and don't destroy it on load
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            Db = FirebaseFirestore.DefaultInstance;
            Debug.Log("Firebase Firestore Initialized");
        });
    }
}
