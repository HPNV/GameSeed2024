using System.Linq;
using Manager;
using Projectile;
using UnityEngine;

namespace Plant.States.Aloecura
{
    public class AloecuraAttackState : PlantAttackState
    {
        private bool _hasSpawnedProjectile;
        private static readonly int AttackTrigger = Animator.StringToHash("Attack");
        public AloecuraAttackState(Plant plant) : base(plant){}

        public override void OnEnter()
        {
            base.OnEnter();
            Plant.Animator.SetTrigger(AttackTrigger);
            _hasSpawnedProjectile = false;
        }

        public override void Update()
        {
            var stateInfo = Plant.Animator.GetCurrentAnimatorStateInfo(0);
            
            if (stateInfo.IsName("Attack") && stateInfo.normalizedTime < 0.5)
                _hasSpawnedProjectile = false;
            
            if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 0.8)
                SpawnProjectile();
            
            if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 1)
                Plant.ChangeState(EPlantState.Idle);
            
        }
        
        private void SpawnProjectile()
        {
            if (_hasSpawnedProjectile)
                return;
            
            _hasSpawnedProjectile = true;
            SoundFXManager.instance.PlayGameSoundOnce("Audio/Plant/Heal");
            
            SingletonGame.Instance.ProjectileManager.Spawn(
                ProjectileName.Aloecura, 
                Plant.transform.position,
                Plant.Data.damage
            );
        }
    }
}