using System;
using System.Collections;
using System.Collections.Generic;
using Plant;
using UnityEngine;

public class PlantFactory : MonoBehaviour
{
    [SerializeField] 
    private GameObject plant;

    [SerializeField] private List<EPlant> plants;
    [SerializeField] private List<PlantData> data;
    private Dictionary<EPlant, PlantData> _plantsData;

    private void Start()
    {
        for (var i = 0; i < plants.Count; i++)
        {
            _plantsData.Add(plants[i], data[i]);
        }
    }

    public GameObject GeneratePlant(EPlant ePlant)
    {
        var obj = Instantiate(plant);
        obj.GetComponent<Plant.Plant>().Data = _plantsData[ePlant];
        return obj;
    }
}