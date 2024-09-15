using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Plant;
using Script;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Plant.Factory
{
    public class PlantFactory : MonoBehaviour
    {
        [SerializeField] 
        private GameObject plant;

        [SerializeField] private List<EPlant> plants;
        [SerializeField] private List<PlantData> data;
        [SerializeField] private PlantPlacementService plantPlacementService;
        private Dictionary<EPlant, PlantData> _plantsData;

        private const int UNLOCKED = 5;
        private const int STEP = 5;

        private void Start()
        {
            _plantsData = new Dictionary<EPlant, PlantData>();
        }

        public GameObject GeneratePlant(EPlant ePlant)
        {
            var obj = Instantiate(plant);
            obj.GetComponent<Plant>().Data = _plantsData[ePlant];
            obj.GetComponent<Plant>().Init();
            return obj;
        }

        public GameObject GeneratePlant(PlantData plantData)
        {
            var obj = Instantiate(plant);
            obj.GetComponent<Plant>().Data = plantData;
            obj.GetComponent<Plant>().Init();
            return obj;
        }

        public PlantData GetPlantData(EPlant ePlant)
        {
            return _plantsData.GetValueOrDefault(ePlant);
        }

        public EPlant GetRandomEPlant()
        {
            //return plants.Where(p => p == EPlant.Explomato).OrderBy(c => Random.value).FirstOrDefault();
            return plants.OrderBy(c => Random.value).FirstOrDefault();
        }

        public void spawnPlant(EPlant ePlant) 
        {
            var newPlant = GeneratePlant(ePlant).GetComponent<Plant>();
            newPlant.ChangeState(EPlantState.Select);

            if (newPlant.Data.plantType == EPlant.Luckyclover)
            {
                SingletonGame.Instance.ResourceManager.LuckModifier += 0.01;
            }
            
            plantPlacementService.plant = newPlant;
        }

        public void SpawnPlant(PlantData plantData)
        {
            var newPlant = GeneratePlant(plantData).GetComponent<Plant>();
            newPlant.ChangeState(EPlantState.Select);

            if (plantData.plantType == EPlant.Luckyclover)
            {
                SingletonGame.Instance.ResourceManager.LuckModifier += 0.01;
            }

            plantPlacementService.plant = newPlant;
        }

        public List<PlantData> GetUnlockedPlants(int amt)
        {
            // Debug.Log($"unlockedea: {SingletonGame.Instance.AchievementManager.UnlockedEAchievements}");
            // Debug.Log($"count: {SingletonGame.Instance.AchievementManager.UnlockedEAchievements.Count}");
            var count = SingletonGame.Instance.AchievementManager.UnlockedEAchievements.Count;
            var unlocked = UNLOCKED + (count / STEP);
            var temp = data.Take(unlocked).OrderBy(x => Guid.NewGuid()).ToList();
            var res = new List<PlantData>();
            for (int i = 0; i < amt;)
            {
                var rand = SingletonGame.Instance.Random.Next(1, 11);

                var selected = rand switch
                {
                    <= 4 => temp.FirstOrDefault(o => o.rarity == ERarity.Common),
                    <= 7 => temp.FirstOrDefault(o => o.rarity == ERarity.Rare),
                    <= 9 => temp.FirstOrDefault(o => o.rarity == ERarity.Epic),
                    10 => temp.FirstOrDefault(o => o.rarity == ERarity.Legendary),
                    _ => null
                };

                if (selected == null) continue;
                res.Add(selected);
                i++;
            }
            return res;
        }

        public PlantData GetLastUnlockedPlant()
        {
            var count = SingletonGame.Instance.AchievementManager.UnlockedEAchievements.Count;
            var unlocked = UNLOCKED + (count / STEP);
            return data[unlocked - 1];
        }
    }
}
