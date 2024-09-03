using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorSelectorService : MonoBehaviour
{
    void OnFire(InputValue inputValue)
    {  
        Debug.Log(inputValue);
    }
}
