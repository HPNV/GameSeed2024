using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardHandler : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject leaderBoard;
    
    [SerializeField] private AudioClip buttonClick;
    
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera2;

    public void ToggleLeaderBoard()
    {
        SoundFXManager.instance.PlaySoundOnce(buttonClick, transform, 1f);
        if (cinemachineVirtualCamera.Priority == 15)
        {
            cinemachineVirtualCamera.Priority = 20;
            cinemachineVirtualCamera2.Priority = 15;
        }
        else
        {
            cinemachineVirtualCamera.Priority = 15;
            cinemachineVirtualCamera2.Priority = 20;    
        }
        
        // Fade in and out
        if (mainMenu.activeSelf)
        {
            leaderBoard.SetActive(true);
            StartCoroutine(FadeOut(mainMenu));
            StartCoroutine(FadeIn(leaderBoard));
        }
        else
        {
            mainMenu.SetActive(true);
            StartCoroutine(FadeOut(leaderBoard));
            StartCoroutine(FadeIn(mainMenu));
        }
        
        // mainMenu.SetActive(!mainMenu.activeSelf);
        // leaderBoard.SetActive(!leaderBoard.activeSelf);
    }
    
    private IEnumerator FadeOut(GameObject obj)
    {
        CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime;
            yield return null;
        }
        obj.SetActive(false);
    }
    
    private IEnumerator FadeIn(GameObject obj)
    {
        CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
        obj.SetActive(true);
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime;
            yield return null;
        }
    }
}
