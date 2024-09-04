using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlacementService : MonoBehaviour
{
    [SerializeField] 
    private GameGrid gameGrid;

    public void Put(Tile tile, GameObject obj)
    {
        Debug.Log($"slots: {gameGrid.Slots}");
        if (gameGrid.Slots[tile] != null) return;
        obj.transform.position = tile.transform.position;
        gameGrid.Slots[tile] = obj;
    }

    public void Remove(Tile tile)
    {
        gameGrid.Slots[tile] = null;
    }
}
