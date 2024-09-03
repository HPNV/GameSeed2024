using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTileConsumer : TileConsumer
{
    [SerializeField] 
    private SpriteRenderer spriteRenderer;

    private void FixedUpdate()
    {
        var currTile = tileService.GetCurrTile();
        if (currTile)
        {
            transform.position = currTile.transform.position;
        }
    }
}
