using System.Collections;
using System.Collections.Generic;
using Plant;
using UnityEngine;

public class PlantSelectState : PlantState
{
    public PlantSelectState(Plant.Plant plant) : base(plant)
    {
    }

    public override void Update()
    {
        var origin = Plant.transform.position;
        Debug.Log($"Singleton: {SingletonGame.Instance}");
        Debug.Log($"TileProvider: {SingletonGame.Instance.TileProvider}");
        var temp = SingletonGame.Instance.TileProvider.GetCurrTile().transform.position;
        temp.z = origin.z;
        Plant.transform.position = temp;
    }

    public override void OnEnter()
    {
        var sp = Plant.transform.GetComponent<SpriteRenderer>();
        var color = Color.red;
        color.a = 0.2f;
        sp.color = color;

        Plant.transform.GetComponent<Collider2D>().enabled = false;
    }

    public override void OnExit()
    {
        Plant.transform.GetComponent<SpriteRenderer>().color = Color.white;
        
        Plant.transform.GetComponent<Collider2D>().enabled = true;
    }
}
