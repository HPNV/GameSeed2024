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

    private Color _tileColor;

    private void Start()
    {
        _tileColor = spriteRenderer.color;
    }


    private void Update()
    {
        if (tileService.GetCurrTile() == tile)
        {
            spriteRenderer.color = Color.red;
        }
        else
        {
            spriteRenderer.color = _tileColor;    
        }
    }
}
