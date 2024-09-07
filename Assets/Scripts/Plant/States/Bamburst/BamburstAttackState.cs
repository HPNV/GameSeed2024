using System.Linq;
using Manager;
using Projectile;
using UnityEngine;

namespace Plant.States.Bamburst
{
    public class BamburstAttackState : PlantAttackState
    {
        private bool _hasDamaged;
        public BamburstAttackState(Plant plant) : base(plant){}

        public override void OnEnter()
        {
            base.OnEnter();
            _hasDamaged = false;
        }

        public override void Update()
        {
            base.Update();
            
            var stateInfo = Plant.Animator.GetCurrentAnimatorStateInfo(0);
            
            if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 0.5)
            {
                DamageEnemies();
            }
        }
        
        private void DamageEnemies()
        {
            if (_hasDamaged)
                return;
            
            _hasDamaged = true;
            var targets = Plant.TargetService.GetTargets();

            foreach (var target in targets)
            {
                target.Damage(Plant.Data.damage);
            }
        }

        protected override void Attack()
        {
            base.Attack();
            _hasDamaged = false;
        }
    }
}