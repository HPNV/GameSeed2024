using System.Collections;
using System.Collections.Generic;
using Plant;
using UnityEngine;

public abstract class PlantDataAdapter : MonoBehaviour
{
    protected PlantData Data;
    void Start()
    {
        Data = transform.parent.gameObject.GetComponent<Plant.Plant>().Data;
    }
}
