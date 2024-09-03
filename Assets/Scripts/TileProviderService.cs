using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileProviderService : TileService
{
    private Tile _currTile;

    public void setCurrTile(Tile tile)
    {
        this._currTile = tile;
    }

    public override Tile GetCurrTile()
    {
        return this._currTile;
    }
}
