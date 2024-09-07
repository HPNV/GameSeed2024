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
            var plants = plantDetectorService.GetPlantsInRange();
            
     
            plants.RemoveAll(plant => plant is null);
            
            var taunter = plants
                .Select(p => p.GetComponent<Plant.Plant>())
                .FirstOrDefault(p => p is not null 
                     && !p.CurrentState.Equals(EPlantState.Select)
                     && p.Data.plantType.Equals(EPlant.Raflessnare) 
                     && Vector2.Distance(p.transform.position, transform.position) < p.Data.range);
            

            if (taunter is not null)
                return taunter.gameObject;
            
            return plants.FirstOrDefault();
        }
        
        public override List<GameObject> GetTargetsInRange()
        {
            return plantDetectorService.GetPlantsInRange();
        }
    }
}