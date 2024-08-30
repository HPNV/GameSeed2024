using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileProvider : MonoBehaviour
{
    private Tile _currTile;
    private TileProviderService _tileProviderService;

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
        _tileProviderService.setCurrTile(_currTile);
    }
}
