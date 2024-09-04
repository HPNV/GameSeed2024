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
    private Dictionary<Vector2, Tile> _tiles;
    public Dictionary<Tile, GameObject> Slots;
    
    void Start()
    {
        _cam = Camera.main.transform;
        GenerateGrid();
    }
    
    private void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        Slots = new Dictionary<Tile, GameObject>();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var spawnedTile = Instantiate(tilePrefab, new Vector2(x, y), Quaternion.identity);

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);
                spawnedTile.transform.SetParent(transform);

                _tiles[new Vector2(x, y)] = spawnedTile;
                Slots[spawnedTile] = null;
            }
        }

        _cam.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);
    }
}
