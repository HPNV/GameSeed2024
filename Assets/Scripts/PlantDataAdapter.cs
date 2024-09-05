using System.Collections;
using System.Collections.Generic;
using Plant;
using UnityEngine;

public abstract class PlantDataAdapter : MonoBehaviour
{
    [SerializeField]
    protected PlantData data;
    void Start()
    {
        data = transform.parent.gameObject.GetComponent<Plant.Plant>().Data;
    }
}
