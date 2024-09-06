using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sp;
    [SerializeField]
    private Sprite first, second;
    
    public void Init()
    {
        int rand = Random.Range(0, 100);
        if(rand < 70) {
            sp.sprite = Resources.Load<Sprite>("Tile/grass_" + 0);
        } else if(rand < 75){
            rand = Random.Range(1, 5);
            sp.sprite = Resources.Load<Sprite>("Tile/grass_" + rand);
        } else {
            rand = Random.Range(6, 9);
            sp.sprite = Resources.Load<Sprite>("Tile/grass_" + rand);
        }
    }
}
