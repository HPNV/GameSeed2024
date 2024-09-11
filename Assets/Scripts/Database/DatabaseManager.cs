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
        InitializeFirebase();
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
                
                Debug.Log("Firebase Firestore Initialized");
                OnFirebaseInitialized?.Invoke();  // Invoke the event to notify listeners
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
    
    public void AddData(string collectionName, string documentId, Dictionary<string, object> data)
    {
        if (!IsInitialized())
        {
            Debug.LogError("Firestore has not been initialized");
            return;
        }

        DocumentReference docRef = Db.Collection(collectionName).Document(documentId);
        docRef.SetAsync(data).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Data successfully written!");
            }
            else
            {
                Debug.LogError("Failed to write data: " + task.Exception);
            }
        });
    }
    
    public void GetData(string collectionName, string documentId, Action<DocumentSnapshot> callback)
    {
        if (!IsInitialized())
        {
            Debug.LogError("Firestore has not been initialized");
            return;
        }

        DocumentReference docRef = Db.Collection(collectionName).Document(documentId);
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                callback(task.Result);
            }
            else
            {
                Debug.LogError("Failed to get data: " + task.Exception);
            }
        });
    }

    public void UpdateData(string collectionName, string documentId, Dictionary<string, object> data)
    {
        if (!IsInitialized())
        {
            Debug.LogError("Firestore has not been initialized");
            return;
        }

        DocumentReference docRef = Db.Collection(collectionName).Document(documentId);
        docRef.UpdateAsync(data).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Data successfully updated!");
            }
            else
            {
                Debug.LogError("Failed to update data: " + task.Exception);
            }
        });
    }

    public void DeleteData(string collectionName, string documentId)
    {
        if (!IsInitialized())
        {
            Debug.LogError("Firestore has not been initialized");
            return;
        }

        DocumentReference docRef = Db.Collection(collectionName).Document(documentId);
        docRef.DeleteAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Data successfully deleted!");
            }
            else
            {
                Debug.LogError("Failed to delete data: " + task.Exception);
            }
        });
    }
}
