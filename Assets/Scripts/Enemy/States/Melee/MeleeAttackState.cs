using UnityEngine;

namespace Enemy.States.Melee
{
    public class MeleeAttackState : AttackState
    {
        private bool hasPlayedSound;
        private bool _hasAttacked;
        public MeleeAttackState(EnemyBehaviour enemy) : base(enemy){}

        public override void OnEnter()
        {
            base.OnEnter();
            _hasAttacked = false;
            hasPlayedSound = false;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            var stateInfo = Enemy.Animator.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 0.7f)
                Attack();
            
            if (stateInfo.IsName("Attack"))
            {
                if (!hasPlayedSound && stateInfo.normalizedTime < 0.5f)  // Plays the sound in the first half of the attack animation
                {
                    SoundFXManager.instance.PlayGameSoundOnce("Audio/Enemy/Bite Sound");
                    hasPlayedSound = true; 
                }

                if (stateInfo.normalizedTime >= 1)
                        Enemy.ChangeState(State.Move);
            }
            
        }
        
        private void Attack()
        {
            var target = Enemy.PlantTargetService.GetTarget();
            if (_hasAttacked || target is null)
                return;
            
            Debug.Log("HAS ATTACKED");
            _hasAttacked = true;

            var entity = target.GetComponent<Entity>();
            
            if (entity is null)
                return;
            
            entity.Damage(Enemy.enemyData.attackPower);
        }
    }
}
