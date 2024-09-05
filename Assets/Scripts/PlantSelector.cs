using System;
using System.Collections;
using System.Collections.Generic;
using Plant;
using UnityEngine;

public class PlantSelector : MonoBehaviour
{
    [SerializeField] 
    private PlantPlacementService plantPlacementService;
    [SerializeField] 
    private PlantFactory plantFactory;
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            var plant = plantFactory.GeneratePlant(EPlant.Cactharn).GetComponent<Plant.Plant>();
            plant.ChangeState(EPlantState.Select);
            plantPlacementService.plant = plant;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Number 2 is pressed");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Number 3 is pressed");
        }
    }
}
