using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Service
{
    public class PlantTargetProvider : PlantTargetService
    {
        public override GameObject GetTarget()
        {
            var enemies = plantDetectorService.GetPlantsInRange();
            
            enemies.RemoveAll(plant => plant is null);
            
            return enemies.FirstOrDefault();
        }
    }
}