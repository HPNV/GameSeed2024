using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyAI : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    // Update is called once per frame
    void Update()
    {
        var camera = Camera.main;
        var cursorPosition = camera!.ScreenToWorldPoint(Input.mousePosition);
        cursorPosition.z = -0.8f;    
        
        transform.position = cursorPosition;
        
    }
}
