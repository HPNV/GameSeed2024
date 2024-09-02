using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField]
    private PlantData data;
    
    void Start()
    {
        GetComponent<Animator>().runtimeAnimatorController = data.animatorController;
    }

    void Update()
    {
        
    }
}
