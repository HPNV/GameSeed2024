using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTileConsumer : TileConsumer
{
    [SerializeField] 
    private Tile tile;
    [SerializeField] 
    private SpriteRenderer spriteRenderer;

    private void FixedUpdate()
    {
        spriteRenderer.color = tileService.GetCurrTile() == tile ? Color.red : Color.white;
    }
}
