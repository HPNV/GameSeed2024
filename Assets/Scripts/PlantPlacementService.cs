using System.Collections;
using System.Collections.Generic;
using Plant;
using UnityEngine;

public class PlantPlacementService : SelectorService
{
    [SerializeField] 
    private TileService tileService;
    [SerializeField] 
    private GameGrid gameGrid;

    public Plant.Plant plant;
    
    public override void OnPlace()
    {
        if (!plant) return;
        gameGrid.PutOnTile(tileService.GetCurrTile(), plant.gameObject);
        plant.ChangeState(EPlantState.Idle);
    }
}
