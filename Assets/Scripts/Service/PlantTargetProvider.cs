using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Plant;
using UnityEngine;

namespace Service
{
    public class PlantTargetProvider : PlantTargetService
    {
        public override GameObject GetTarget()
        {
            var plants = plantDetectorService
                .GetPlantsInRange()
                .Where(p => p != null)
                .Where(p =>
                {
                    var plant = p.GetComponent<Plant.Plant>();
                    return plant == null || plant.CurrentState != EPlantState.Select;
                })
                .ToList();
            
            
            var taunter = plants
                .Select(p => p.GetComponent<Plant.Plant>())
                .FirstOrDefault(p => p != null 
                     && p.Data.plantType.Equals(EPlant.Raflessnare) 
                     && Vector2.Distance(p.transform.position, transform.position) < p.Data.range);
            

            if (taunter is not null)
                return taunter.gameObject;

            return plants.FirstOrDefault()?.gameObject;
        }
        
        public override List<GameObject> GetTargetsInRange()
        {
            return plantDetectorService
                .GetPlantsInRange()
                .Where(p => p != null)
                .Where(p =>
                {
                    var plant = p.GetComponent<Plant.Plant>();
                    return plant == null || plant.CurrentState != EPlantState.Select;
                })
                .ToList();
        }
    }
}