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
        return (plant != null) && (gameGrid.ValidateSlot(tileService.GetCurrTile()));
    }

    protected override void OnPlace()
    {
        gameGrid.PutOnTile(tileService.GetCurrTile(), plant.gameObject);

        if (plant.Data.plantType == EPlant.Aloecura)
        {
            SingletonGame.Instance.PlayerManager.OnPlantAlocure();
        } else if (plant.Data.plantType == EPlant.Cocowall)
        {
            SingletonGame.Instance.PlayerManager.OnPlantCocoWall();
        }
    }

    protected override void AfterPlace()
    {
        plant.ChangeState(EPlantState.Grow);
        plant = null;
        SingletonGame.Instance.PlayerManager.OnPlantPlanted();
    }
}
