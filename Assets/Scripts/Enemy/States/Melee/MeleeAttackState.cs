using UnityEngine;

namespace Enemy.States.Melee
{
    public class MeleeAttackState : AttackState
    {
        public MeleeAttackState(EnemyBehaviour enemy) : base(enemy){} 
        
        
        public override void OnUpdate()
        {
            base.OnUpdate();
            var stateInfo = Enemy.Animator.GetCurrentAnimatorStateInfo(0);
            SoundFXManager.instance.PlayGameSoundOnce(Resources.Load<AudioClip>("Audio/Enemy/Bite Sound"));
            if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 1)
            {
                Enemy.ChangeState(State.Move);
            }
        }
    }
}