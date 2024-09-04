using Projectile;
using UnityEngine;

namespace Enemy.States.Explosive
{
    public class ExplosiveExplodeState : BaseState
    {
        private static readonly int Explode = Animator.StringToHash("Explode");

        public ExplosiveExplodeState(EnemyBehaviour enemy) : base(enemy){}


        public override void OnEnter()
        {
            Enemy.Animator.SetTrigger(Explode);
        }

        public override void OnUpdate()
        {
            var stateInfo = Enemy.Animator.GetCurrentAnimatorStateInfo(0);
            
            if (stateInfo.IsName("Explode") && stateInfo.normalizedTime >= 1)
                Object.Destroy(Enemy.gameObject);
        }
        
    }
}