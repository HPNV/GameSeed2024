using System.Collections.Generic;
using System.Linq;
using Projectile;
using UnityEngine;

namespace Enemy.States.Explosive
{
    public class ExplosiveExplodeState : BaseState
    {
        private List<Entity> _entitiesInRange = new();
        private bool _hasDamaged;
        private static readonly int Explode = Animator.StringToHash("Explode");

        public ExplosiveExplodeState(EnemyBehaviour enemy) : base(enemy){}


        public override void OnEnter()
        {
            Enemy.Animator.SetTrigger(Explode);
            GetEnemiesInExplodeRange();
            _hasDamaged = false;
        }

        public override void OnUpdate()
        {
            var stateInfo = Enemy.Animator.GetCurrentAnimatorStateInfo(0);
            
            if (stateInfo.IsName("Explode") && stateInfo.normalizedTime >= 0.7f && !_hasDamaged)
            {
                _hasDamaged = true;
                foreach (var entity in _entitiesInRange)
                {
                    if (entity is not null)
                        entity.Damage(Enemy.enemyData.attackPower);
                }
            }
            
            if (stateInfo.IsName("Explode") && stateInfo.normalizedTime >= 1) {
                SoundFXManager.instance.PlayGameSoundOnce("Audio/Explode");
                Object.Destroy(Enemy.gameObject);
                SingletonGame.Instance.PlayerManager.OnEnemyExplode();
            }
        }


        private void GetEnemiesInExplodeRange()
        {
            var objects =  Enemy.PlantTargetService.GetTargetsInRange();

            _entitiesInRange = objects.Where(p =>
            {
                var distance = Vector2.Distance(Enemy.transform.position, p.transform.position);
                return distance <= Enemy.enemyData.damageRange;
            }).Select(p => p.GetComponent<Entity>()).ToList();
        }
    }
}