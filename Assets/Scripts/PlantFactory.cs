using System.Collections;
using System.Collections.Generic;
using Plant;
using UnityEngine;

public class PlantFactory : MonoBehaviour
{
    [SerializeField] 
    private GameObject plant;

    [SerializeField] 
    private SerializableDictionary<EPlant, PlantData> plantsData;
}
