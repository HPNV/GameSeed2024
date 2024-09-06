using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Plant;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Factory
{
    public class PlantFactory : MonoBehaviour
    {
        [SerializeField] 
        private GameObject plant;

        [SerializeField] private List<EPlant> plants;
        [SerializeField] private List<PlantData> data;
        [SerializeField] private PlantPlacementService plantPlacementService;
        private Dictionary<EPlant, PlantData> _plantsData;

        private void Start()
        {
            _plantsData = new Dictionary<EPlant, PlantData>(); 
            for (var i = 0; i < plants.Count; i++)
            {
                _plantsData.Add(plants[i], data[i]);
            }
        }

        public GameObject GeneratePlant(EPlant ePlant)
        {
            var obj = Instantiate(plant);
            obj.GetComponent<Plant.Plant>().Data = _plantsData[ePlant];
            obj.GetComponent<Plant.Plant>().Init();
            return obj;
        }

        public PlantData GetPlantData(EPlant ePlant)
        {
            return _plantsData.GetValueOrDefault(ePlant);
        }

        public EPlant GetRandomEPlant()
        {
            return plants.OrderBy(c => Random.value).FirstOrDefault();
        }

        public void spawnPlant(EPlant ePlant) 
        {
            var plant = GeneratePlant(ePlant).GetComponent<Plant.Plant>();
            plant.ChangeState(EPlantState.Select);
            plantPlacementService.plant = plant;
        }
    }
}
