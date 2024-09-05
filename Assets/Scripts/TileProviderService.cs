using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileProviderService : TileService
{
    private Tile _currTile;

    private void FixedUpdate()
    {
        
    }

    public void SetCurrTile(Tile tile)
    {
        this._currTile = tile;
    }

    public override Tile GetCurrTile()
    {
        return this._currTile;
    }
}
