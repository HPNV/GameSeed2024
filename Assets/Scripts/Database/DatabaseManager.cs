using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Extensions;
using Firebase.Firestore;
public class DatabaseManager : MonoBehaviour
{
    private void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
            Debug.Log("Firebase Firestore Initialized");
        });
    }
}
