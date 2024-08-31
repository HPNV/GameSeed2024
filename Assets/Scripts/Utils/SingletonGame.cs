using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

public class SingletonGame : MonoBehaviour
{
    public static SingletonGame Instance { get; private set; }
    [SerializeField] public HomeBase homeBase;
    public int ExpPoint;
    
    public ResourceManager ResourceManager { get; set; } = new();
    public ExperienceManager ExperienceManager { get; set; } = new();


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Set the instance to this object
            ResourceManager.Initialize();
            ExperienceManager.Initialize();
            DontDestroyOnLoad(gameObject); // Make the object persistent across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy the duplicate instance
        }
    }

    void Start() {

    }
}
