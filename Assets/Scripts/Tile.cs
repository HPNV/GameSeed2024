using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sp;
    [SerializeField]
    private Sprite first, second;
    
    public void Init(bool isSecond)
    {
        sp.sprite = isSecond ? first : second;
    }
}
