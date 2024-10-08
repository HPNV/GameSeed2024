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
        var pos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        var key = new Vector2(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y));
        
        // Debug.Log($"pos: {pos}");
        // Debug.Log($"key: {key}");
        
        return gameGrid.Tiles.GetValueOrDefault(key);
    }
}
