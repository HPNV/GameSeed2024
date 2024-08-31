using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayButtonHandler : MonoBehaviour
{
    
    public GameObject LoadingScreen;
    public Image LoadingBarFill;

    public void PlayGame(int sceneId)
    {
        StartCoroutine(LoadScene(sceneId));
    }

    IEnumerator LoadScene(int sceneId)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneId);

        LoadingScreen.SetActive(true);

        while (!asyncLoad.isDone)
        {
            float loadProgress = Mathf.Clamp01(asyncLoad.progress / 0.9f);

            LoadingBarFill.fillAmount = loadProgress;
        
            yield return null;
        }
    }
}
