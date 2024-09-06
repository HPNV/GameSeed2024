using UnityEngine;

namespace Enemy.States.Melee
{
    public class MeleeAttackState : AttackState
    {
        private bool _hasAttacked;
        public MeleeAttackState(EnemyBehaviour enemy) : base(enemy){}

        public override void OnEnter()
        {
            base.OnEnter();
            _hasAttacked = false;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            var stateInfo = Enemy.Animator.GetCurrentAnimatorStateInfo(0);
            
            if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 0.7f)
                Attack();
            
            if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 1)
                Enemy.ChangeState(State.Move);
            
        }
        
        private void Attack()
        {
            var target = Enemy.PlantTargetService.GetTarget();
            if (_hasAttacked || target is null)
                return;
            
            Debug.Log("HAS ATTACKED");
            _hasAttacked = true;

            var plant = target.GetComponent<Plant.Plant>();
            
            if (plant is null)
                return;
            
            target.GetComponent<Plant.Plant>().Damage(Enemy.enemyData.attackPower);
        }
    }
}