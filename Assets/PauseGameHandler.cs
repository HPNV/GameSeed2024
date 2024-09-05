using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGameHandler : MonoBehaviour
{
    
    public void TogglePauseGame()
    {
        gameObject.SetActive(!gameObject.activeSelf);

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            return;
        }
        
        Time.timeScale = 0;
    }
    
    public void QuitGame()
    {
        
    }
    
}
