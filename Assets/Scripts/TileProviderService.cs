using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileProviderService : TileService
{
    private Tile _currTile;

    void Start()
    {
        _currTile = GetComponent<Tile>();

        if (_currTile == null)
        {
            Debug.LogError("TileProvider fail to find Tile component not found on the GameObject.");
        }
    }

    public override Tile GetCurrTile()
    {
        return _currTile;
    }
}
