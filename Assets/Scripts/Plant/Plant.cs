using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField]
    private PlantData data;
    private TargetService targetService;
    
    void Start()
    {
        GetComponent<Animator>().runtimeAnimatorController = data.animatorController;
        
        InitTargetService();
    }

    void Update()
    {
        
    }

    private void InitTargetService()
    {
        switch (data.targetType)
        {
            case TargetType.Single: 
                targetService = GetComponent<SingleTargetProvider>();
                break;
            case TargetType.Multi:
                break;
            default:
                break;
        }
    }
}
