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
        throw new System.NotImplementedException();
    }

    public override void OnEnter()
    {
        var sp = Plant.transform.GetComponent<SpriteRenderer>();
        var color = Color.red;
        color.a = 0.2f;
        sp.color = color;
    }

    public override void OnExit()
    {
        Plant.transform.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
