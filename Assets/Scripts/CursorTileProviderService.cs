using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorTileProviderService : TileService
{
    private Camera _mainCamera;
    [SerializeField] 
    private GameGrid gameGrid;
    
    private void Start()
    {
        _mainCamera = Camera.main;
    }
    
    public override Tile GetCurrTile()
    {
        var pos = Vector3Int.FloorToInt(_mainCamera.ScreenToWorldPoint(Input.mousePosition));
        return gameGrid.Tiles.GetValueOrDefault(new Vector2(pos.x, pos.y));
    }
}
