using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitSceneHandler : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Image LoadingBarFill;
    public int sceneId;

    public void PlayGame(int sceneId)
    {
        Console.WriteLine("Loading scene " + sceneId);
        this.sceneId = sceneId;

        StartCoroutine(LoadScene(sceneId));
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
