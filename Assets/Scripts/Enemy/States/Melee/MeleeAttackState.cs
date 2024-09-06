using UnityEngine;

namespace Enemy.States.Melee
{
    public class MeleeAttackState : AttackState
    {
        private bool hasPlayedSound;

        public MeleeAttackState(EnemyBehaviour enemy) : base(enemy)
        {
            hasPlayedSound = false;  // Initialize the flag to false
        }

        
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

            // Get the current animation state info
            var stateInfo = Enemy.Animator.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 0.7f)
                Attack();
            
            if (stateInfo.IsName("Attack"))
            {
                // Play the sound once at the beginning of the attack
                if (!hasPlayedSound && stateInfo.normalizedTime < 0.5f)  // Plays the sound in the first half of the attack animation
                {
                    SoundFXManager.instance.PlayGameSoundOnce(Resources.Load<AudioClip>("Audio/Enemy/Bite Sound"));
                    hasPlayedSound = true;  // Set the flag to true after playing the sound
                }

                // Transition to Move state when attack animation is complete
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

            var plant = target.GetComponent<Plant.Plant>();
            
            if (plant is null)
                return;
            
            target.GetComponent<Plant.Plant>().Damage(Enemy.enemyData.attackPower);
        }
    }
}
