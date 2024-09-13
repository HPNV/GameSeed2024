using System.Linq;
using Manager;
using Projectile;
using UnityEngine;

namespace Plant.States.Swiftglory
{
    public class SwiftgloryAttackState : PlantAttackState
    {
        private bool _hasSpawnedProjectile;
        private static readonly int AttackTrigger = Animator.StringToHash("Attack");
        public SwiftgloryAttackState(Plant plant) : base(plant){}

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
            
            if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 0.5)
                SpawnProjectile();
            
            if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 1)
                Plant.ChangeState(EPlantState.Idle);
            
        }
        
        private void SpawnProjectile()
        {
            if (_hasSpawnedProjectile)
                return;
            
            _hasSpawnedProjectile = true;
            //TODO GANTI
            SoundFXManager.instance.PlayGameSoundOnce("Audio/Plant/Heal");
            
            SingletonGame.Instance.ProjectileManager.Spawn(
                ProjectileName.Aloecure, 
                Plant.transform.position,
                Plant.Data.damage
            );
        }
    }
}