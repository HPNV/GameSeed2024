using System.Collections.Generic;
using System.Linq;
using Projectile;
using UnityEngine;

namespace Enemy.States.Explosive
{
    public class ExplosiveExplodeState : BaseState
    {
        private List<Plant.Plant> _plantsInRange = new();
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
                foreach (var plant in _plantsInRange)
                    plant.Damage(Enemy.enemyData.attackPower);
            }
            
            if (stateInfo.IsName("Explode") && stateInfo.normalizedTime >= 1) {
                SoundFXManager.instance.PlayGameSoundOnce(Resources.Load<AudioClip>("Audio/Explode"));
                Object.Destroy(Enemy.gameObject);
            }
        }


        private void GetEnemiesInExplodeRange()
        {
            var plants =  Enemy.PlantTargetService.GetTargetsInRange();

            _plantsInRange = plants.Where(p =>
            {
                var distance = Vector2.Distance(Enemy.transform.position, p.transform.position);
                return distance <= Enemy.enemyData.damageRange;
            }).Select(p => p.GetComponent<Plant.Plant>()).ToList();
        }
    }
}