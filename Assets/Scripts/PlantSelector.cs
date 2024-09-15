using System;
using System.Collections;
using System.Collections.Generic;
using Plant;
using Plant.Factory;
using UnityEngine;

public class PlantSelector : MonoBehaviour
{
    [SerializeField] 
    private PlantPlacementService plantPlacementService;
    [SerializeField] 
    private PlantFactory plantFactory;
    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Alpha1))
    //     {
    //         var plant = plantFactory.GeneratePlant(EPlant.Boomkin).GetComponent<Plant.Plant>();
    //         plant.ChangeState(EPlantState.Select);
    //         plantPlacementService.plant = plant;
    //     }
    //     else if (Input.GetKeyDown(KeyCode.Alpha2))
    //     {
    //         var plant = plantFactory.GeneratePlant(EPlant.Cactharn).GetComponent<Plant.Plant>();
    //         plant.ChangeState(EPlantState.Select);
    //         plantPlacementService.plant = plant;
    //     }
    //     else if (Input.GetKeyDown(KeyCode.Alpha3))
    //     {
    //         var plant = plantFactory.GeneratePlant(EPlant.Cobcorn).GetComponent<Plant.Plant>();
    //         plant.ChangeState(EPlantState.Select);
    //         plantPlacementService.plant = plant;
    //     }
    //     else if (Input.GetKeyDown(KeyCode.Alpha4))
    //     {
    //         var plant = plantFactory.GeneratePlant(EPlant.Raflessnare).GetComponent<Plant.Plant>();
    //         plant.ChangeState(EPlantState.Select);
    //         plantPlacementService.plant = plant;
    //     }
    //     else if (Input.GetKeyDown(KeyCode.Alpha5))
    //     {
    //         var plant = plantFactory.GeneratePlant(EPlant.Weisshooter).GetComponent<Plant.Plant>();
    //         plant.ChangeState(EPlantState.Select);
    //         plantPlacementService.plant = plant;
    //     }
    // }
}
