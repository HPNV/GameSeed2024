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

    protected override void OnPlace()
    {
        if (plant == null) return;
        gameGrid.PutOnTile(tileService.GetCurrTile(), plant.gameObject);
    }

    protected override void AfterPlace()
    {
        plant.ChangeState(EPlantState.Idle);
        plant = null;
    }
}
