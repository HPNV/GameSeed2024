using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayButtonHandler : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Image LoadingBarFill;
    public Cutscene cutscene;
    public int sceneId;

    public void PlayGame(int sceneId)
    {
        Console.WriteLine("Loading scene " + sceneId);
        cutscene.StartCutscene();
        this.sceneId = sceneId;
        
    }

    public void Update()
    {
        if(cutscene.isFinished) {
            cutscene.isFinished = false;
            StartCoroutine(LoadScene(sceneId));
        }
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
