using System;
using System.Collections.Generic;
using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine;
using TMPro;

public class LeaderBoardData : MonoBehaviour
{
    private FirebaseFirestore db;
    private List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
    
    [SerializeField] private GameObject leaderBoardPanel;
    
    private void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        
        // Fetch data from Firestore
        GetLeaderBoardData();
    }

    private void GetLeaderBoardData()
    {
        CollectionReference docRef = db.Collection("users");
        
        Query query = docRef.OrderByDescending("highest_score").Limit(10);
        
        // Get the highest_score of each user and sort it descendingly, then take the 10 highest scores
        query.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                QuerySnapshot snapshot = task.Result;
                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    string id = document.Id;
                    
                    Dictionary<string, object> userData = document.ToDictionary();
                    userData.Add("id", id);
                    
                    data.Add(userData);
                }
                
                // Now that the data is fetched, bind it to the UI
                bindData();
            }
            else
            {
                Debug.LogError("Error getting documents: " + task.Exception);
            }
        });
    }

    private void bindData()
    {
        // Check if data is available before binding
        if (data.Count == 0)
        {
            Debug.LogWarning("No data available to bind.");
            return;
        }

        int i = 0;
        foreach (Transform child in leaderBoardPanel.transform)
        {
            if (i >= data.Count)
            {
                Debug.LogWarning("More UI rows than available data");
                break;
            }

            // Assuming each child of leaderBoardPanel is a row, and children hold TextMeshProUGUI components.
            // Also assuming that the first and second children are the ID and Score respectively.
            TextMeshProUGUI[] texts = child.GetComponentsInChildren<TextMeshProUGUI>();

            texts[1].text = data[i]["id"].ToString();
            texts[2].text = data[i]["highest_score"].ToString();

            i++;
        }

        Debug.Log("Data successfully bound to leaderboard.");
    }
}
