using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileProvider : MonoBehaviour
{
    private Tile _currTile;
    [field: SerializeField]
    private TileProviderService TileProviderService { get; set; }

    void Start()
    {
        _currTile = GetComponent<Tile>();

        if (_currTile == null)
        {
            Debug.LogError("TileProvider fail to find Tile component not found on the GameObject.");
        }
    }

    private void OnMouseEnter()
    {
        TileProviderService.setCurrTile(_currTile);
    }

    private void OnMouseExit()
    {
        TileProviderService.setCurrTile(null);
    }
}
