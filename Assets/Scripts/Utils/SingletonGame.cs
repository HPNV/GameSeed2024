using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SIngletonGame : MonoBehaviour
{
    public static SIngletonGame Instance { get; private set; }
    public  int experiencePoint;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Set the instance to this object
            DontDestroyOnLoad(gameObject); // Make the object persistent across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy the duplicate instance
        }
    }

    void Start() {
        experiencePoint = 0;
    }

    public void setExp(int exp) {
        experiencePoint = exp;
    }

    
}
