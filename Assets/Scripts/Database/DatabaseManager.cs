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
    
    public void AddData(string collectionName, string documentId, Dictionary<string, object> data)
    {
        if (Db == null)
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
        if (Db == null)
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
        if (Db == null)
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
        if (Db == null)
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
