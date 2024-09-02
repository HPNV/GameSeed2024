using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Plant
{
    public class PlantAttackState : PlantState
    {
        private static readonly int AttackTriggerHash = Animator.StringToHash("Attack");

        private int cd = 0;
        
        public PlantAttackState(Plant plant) : base(plant)
        {
            
        }

        public override void Update()
        {
            if (Plant.TargetService.GetTargets().Count == 0)
            { 
                Plant.ChangeState(EPlantState.Idle);
            }

            cd++;
            if (cd != Plant.Data.cd) return;
            Attack();
            cd = 0;
        }

        private void Attack()
        {
            Plant.Animator.SetTrigger(AttackTriggerHash);
        }
    }
}