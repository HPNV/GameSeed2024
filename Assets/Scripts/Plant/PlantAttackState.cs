using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Plant
{
    public class PlantAttackState : PlantState
    {
        protected float CoolDown;
        private static readonly int AttackTrigger = Animator.StringToHash("Attack");

        public PlantAttackState(Plant plant) : base(plant){}

        public override void Update()
        {
            var targets = Plant.TargetService.GetTargets();
            
            Debug.Log("Targets: " + targets.Count);
            if (targets.Count == 0)
            { 
                Plant.ChangeState(EPlantState.Idle);
            }

            CoolDown += Time.deltaTime;
            
            if (CoolDown < Plant.Data.attackCooldown) 
                return;
            
            Attack();
        }

        public override void OnEnter()
        {
        }

        public override void OnExit()
        {
        }

        private void Attack()
        {
            Plant.Animator.SetTrigger(AttackTrigger);
            CoolDown = 0;
        }
    }
}