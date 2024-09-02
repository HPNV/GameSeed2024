using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Plant
{
    public class PlantAttackState : PlantState
    {
        public PlantAttackState(Plant plant) : base(plant)
        {
            
        }

        public override void Update()
        {
            if (Plant.TargetService.GetTargets().Count == 0)
            { 
                Plant.ChangeState(EPlantState.Idle);
            }
        }
    }
}