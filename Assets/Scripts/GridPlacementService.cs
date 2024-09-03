using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlacementService : MonoBehaviour
{
    [SerializeField] 
    private GameGrid gameGrid;

    private Dictionary<Tile, GameObject> _slots;

    public void Put(Tile tile, GameObject obj)
    {
        if (!_slots[tile])
        {
            obj.transform.position = tile.transform.position;
            _slots[tile] = obj;
        }
    }

    public void Remove(Tile tile)
    {
        _slots[tile] = null;
    }
}
