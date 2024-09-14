using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class InGameAchievement : MonoBehaviour
{
    public GameObject leaderBoard;
    
    [SerializeField] private AudioClip buttonClick;
    
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera2;

    public void ToggleLeaderBoard()
    {
        SoundFXManager.instance.PlaySoundOnce(buttonClick, transform, 1f);
        if (cinemachineVirtualCamera.Priority == 10)
        {
            cinemachineVirtualCamera.Priority = 5;
            cinemachineVirtualCamera2.Priority = 10;
        }
        else
        {
            cinemachineVirtualCamera.Priority = 10;
            cinemachineVirtualCamera2.Priority = 5;    
        }
        
        // if (leaderBoard.activeSelf)
        // {
        //     leaderBoard.SetActive(false);
        // }
        // else
        // {
        //     leaderBoard.SetActive(true);
        // }
        
        // mainMenu.SetActive(!mainMenu.activeSelf);
        Time.timeScale = leaderBoard.activeSelf ? 1 : 0;
        leaderBoard.SetActive(!leaderBoard.activeSelf);
    }
}
