using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    [SerializeField] 
    private int width, height;
    [SerializeField]
    private int offsetX, offsetY;
    
    private Transform _cam;

    [SerializeField] 
    private Tile tilePrefab;
    public Dictionary<Vector2, Tile> Tiles { get; private set; }
    public Dictionary<Tile, GameObject> Slots { get; private set; }
    [SerializeField] private List<Sprite> cornerSprites;
    [SerializeField] private List<Sprite> sideSpritesTop;
    [SerializeField] private List<Sprite> sideSpritesLeft;
    [SerializeField] private List<Sprite> sideSpritesRight;
    [SerializeField] private List<Sprite> sideSpritesBottom;
    [SerializeField] private int sideSpritesCount;
    
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
                int a = x + offsetX;
                int b = y + offsetY;
                var spawnedTile = Instantiate(tilePrefab, new Vector3(a, b, 200), Quaternion.identity);

                var isOffset = (a % 2 == 0 && b % 2 != 0) || (a % 2 != 0 && b % 2 == 0);
                spawnedTile.Init();
                spawnedTile.transform.SetParent(transform);

                Tiles[new Vector2(a, b)] = spawnedTile;
                Slots[spawnedTile] = null;

                if (x == 0 && y == 0)
                    spawnedTile.transform.Find("Deco").gameObject.GetComponent<SpriteRenderer>().sprite =
                        cornerSprites[3];
                else if (x == 0 && y == height - 1)
                    spawnedTile.transform.Find("Deco").gameObject.GetComponent<SpriteRenderer>().sprite =
                        cornerSprites[0];
                else if (x == width - 1 && y == 0)
                    spawnedTile.transform.Find("Deco").gameObject.GetComponent<SpriteRenderer>().sprite =
                        cornerSprites[2];
                else if (x == width - 1 && y == height - 1)
                    spawnedTile.transform.Find("Deco").gameObject.GetComponent<SpriteRenderer>().sprite =
                        cornerSprites[1];
                else if (x == 0)
                    spawnedTile.transform.Find("Deco").gameObject.GetComponent<SpriteRenderer>().sprite =
                        sideSpritesLeft[Random.Range(0, sideSpritesCount)];
                else if (y == height - 1)
                    spawnedTile.transform.Find("Deco").gameObject.GetComponent<SpriteRenderer>().sprite =
                        sideSpritesTop[Random.Range(0, sideSpritesCount)];
                else if (y == 0)
                    spawnedTile.transform.Find("Deco").gameObject.GetComponent<SpriteRenderer>().sprite =
                        sideSpritesBottom[Random.Range(0, sideSpritesCount)];
                else if (x == width - 1)
                    spawnedTile.transform.Find("Deco").gameObject.GetComponent<SpriteRenderer>().sprite =
                        sideSpritesRight[Random.Range(0, sideSpritesCount)];
            }
        }
        
        
        
        // var temp = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);
        // _cam.transform.position  = temp;
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
