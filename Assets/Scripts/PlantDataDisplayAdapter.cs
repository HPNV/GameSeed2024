using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantDataDisplayAdapter : PlantDataAdapter
{
    [SerializeField] 
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer.sprite = Data.sprite;
    }
}
