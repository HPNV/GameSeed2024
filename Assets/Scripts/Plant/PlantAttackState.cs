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
<<<<<<< Updated upstream

            cd++;
            if (cd != Plant.Data.cd) return;
=======
            
            CoolDown += Time.deltaTime;
            
            if (CoolDown < Plant.Data.attackCooldown) 
                return;
            
>>>>>>> Stashed changes
            Attack();
            cd = 0;
        }

        public override void OnEnter()
        {
        }

        public override void OnExit()
        {
        }

        private void Attack()
        {
            Plant.Animator.SetTrigger(AttackTriggerHash);
        }
    }
}