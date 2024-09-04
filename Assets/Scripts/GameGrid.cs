using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    [SerializeField] 
    private int width, height;
    
    private Transform _cam;

    [SerializeField] 
    private Tile tilePrefab;
    public Dictionary<Vector2, Tile> Tiles { get; private set; }
    public Dictionary<Tile, GameObject> Slots { get; private set; }
    
    void Start()
    {
        _cam = Camera.main.transform;
        GenerateGrid();
    }
    
    private void GenerateGrid()
    {
        Tiles = new Dictionary<Vector2, Tile>();
        Slots = new Dictionary<Tile, GameObject>();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var spawnedTile = Instantiate(tilePrefab, new Vector2(x, y), Quaternion.identity);

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);
                spawnedTile.transform.SetParent(transform);

                Tiles[new Vector2(x, y)] = spawnedTile;
                Slots[spawnedTile] = null;
            }
        }

        
        _cam.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);
    }
    
    public void PutOnTile(Tile tile, GameObject obj)
    {
        if (Slots[tile] != null) return;
        var origin = obj.transform.position;
        var temp = tile.transform.position;
        temp.z = origin.z;
        obj.transform.position = temp;
        Slots[tile] = obj;
    }

    public void RemoveFromTile(Tile tile)
    { 
        Slots[tile] = null;
    }
}
