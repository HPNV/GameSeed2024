using System.Collections;
using System.Collections.Generic;
using Plant;
using UnityEngine;

public class PlantPlacementService : SelectorService
{
    [SerializeField] 
    private TileService tileService;
    [SerializeField] 
    private GridPlacementService gridPlacementService;

    public Plant.Plant plant;
    
    public override void OnPlace()
    {
        Debug.Log($"grid: {gridPlacementService}");
        Debug.Log($"tileService: {tileService}");
        Debug.Log($"tile: {tileService.GetCurrTile()}");
        Debug.Log($"plantComp: {plant}");
        Debug.Log($"plant: {plant.gameObject}");
        gridPlacementService.Put(tileService.GetCurrTile(), plant.gameObject);
        plant.ChangeState(EPlantState.Idle);
    }
}
