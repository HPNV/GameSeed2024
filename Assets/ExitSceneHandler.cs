using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitSceneHandler : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Image LoadingBarFill;
    public int sceneId;

    private FirebaseFirestore db;
    
    public void PlayGame(int sceneId)
    {
        
        db = FirebaseFirestore.DefaultInstance;
        
        HighScoreUpdate();
        
        this.sceneId = sceneId;

        StartCoroutine(LoadScene(sceneId));
    }
    
    private void HighScoreUpdate()
    {
        string hostname = System.Environment.MachineName;
        db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("users").Document(hostname);
        
        // get the user highest_score then compare it to the current score
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DocumentSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    Dictionary<string, object> data = snapshot.ToDictionary();
                    int highest_score = (int) data["highest_score"];
                    if (SingletonGame.Instance.homeBase.score > highest_score)
                    {
                        UpdateHighScore(docRef);
                    }
                }
            }
        });
    }

    private void UpdateHighScore(DocumentReference docRef)
    {
        
        Dictionary<string, object> data = new Dictionary<string, object>
        {
            {"highest_score", SingletonGame.Instance.homeBase.score}
        };
        docRef.SetAsync(data).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("High Score Updated");
            }
        });
    }

    IEnumerator LoadScene(int sceneId)
    {
        // Now load the scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneId);

        // Deactivate automatic scene activation
        asyncLoad.allowSceneActivation = false;

        LoadingScreen.SetActive(true);
        
        // Update the loading bar as the scene loads
        while (asyncLoad.progress < 0.9f)
        {
            float loadProgress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            LoadingBarFill.fillAmount = loadProgress;
            yield return null;
        }
        
        StopAllCoroutines();
        Time.timeScale = 1;
        asyncLoad.allowSceneActivation = true;
        
    }
}
