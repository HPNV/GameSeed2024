using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Plant.States
{
    public class PlantIdleState : PlantState
    {
        public PlantIdleState(Plant plant) : base(plant)
        {
            
        }

        public override void Update()
        {
            if (Plant.TargetService.GetTargets().Count > 0)
            { 
                Plant.ChangeState(EPlantState.Attack);
            }
        }

        public override void OnEnter()
        {
        }

        public override void OnExit()
        {
        }
    }
}
