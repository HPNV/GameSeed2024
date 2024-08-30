using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitHandler : MonoBehaviour
{
    public Button quitButton;

    public void Start()
    {
        Application.Quit();
    }
}
