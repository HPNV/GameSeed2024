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

    protected override bool Validate()
    {
        return plant != null;
    }

    protected override void OnPlace()
    {
        gameGrid.PutOnTile(tileService.GetCurrTile(), plant.gameObject);
    }

    protected override void AfterPlace()
    {
        plant.ChangeState(EPlantState.Grow);
        plant = null;
    }
}
