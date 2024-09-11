using System;
using System.Collections.Generic;
using Enemy;
using Unity.VisualScripting;
using UnityEngine;
using State = Enemy.States.State;

namespace Projectile.Behaviour
{
    public class ExplosiveMortarProjectileBehaviour : ProjectileBaseBehaviour
    {
        private static readonly int Exploding = Animator.StringToHash("Explode");
        private List<EnemyBehaviour> _attackEnemies = new();
        private readonly float _inAirTime;
        private readonly float _speed;
        private readonly float _initialSpeedSprite;
        private float _timeAlive;
        private bool _hasExploded;
        private const float Gravity = 10f;

        public ExplosiveMortarProjectileBehaviour(Projectile projectile) : base(projectile)
        {
            _inAirTime = 2f;
            var totalDistance = Vector2.Distance(Projectile.transform.position, Projectile.Target);
            _speed = totalDistance / _inAirTime;
            _timeAlive = 0;
            _initialSpeedSprite = 10;
            _hasExploded = false;
            Debug.Log("RE CREATED");
        }

        public override void Update()
        {
            if (SingletonGame.Instance.IsPaused)
                return;
            
            var distanceToMove = _speed * Time.deltaTime;
           
            Projectile.transform.position = Vector2.MoveTowards(
                Projectile.transform.position,
                Projectile.Target,
                distanceToMove
            );

            _timeAlive += Time.deltaTime;
            
            var ySprite = _initialSpeedSprite * _timeAlive - Gravity * _timeAlive * _timeAlive / 2;
            
            
            Projectile.SpriteObject.transform.localPosition = new Vector3(0, Math.Max(ySprite, 0), 0);
            
            if (_timeAlive >= _inAirTime)
            {
                if (!_hasExploded)
                {
                    Projectile.Animator.SetTrigger(Exploding);
                    _hasExploded = true;
                    
                    Damage();
                }
                
                Projectile.SpriteObject.transform.localScale += new Vector3(0.05f, 0.05f, 0);
                
                var stateInfo = Projectile.Animator.GetCurrentAnimatorStateInfo(0);
                
                if (stateInfo.IsName("Explode") && stateInfo.normalizedTime >= 0.9)
                {
                    SingletonGame.Instance.ProjectileManager.Despawn(Projectile);
                }
            }
        }

        public override void OnCollide(Collision2D collider)
        {
            var enemy = collider.gameObject.GetComponent<EnemyBehaviour>();
            
            if (enemy is null)
                return;
            
            if (enemy.CurrentState == State.Die)
                return;
            
            Physics2D.IgnoreCollision(Projectile.GetComponent<Collider2D>(), collider.collider);
            _attackEnemies.Add(enemy);
        }

        private void Damage()
        {
            foreach (var enemy in _attackEnemies)
            {
                var distance = Vector2.Distance(Projectile.transform.position, enemy.transform.position);

                if (distance <= Projectile.data.attackRadius)
                {
                    enemy.Damage(Projectile.AttackPower);
                }
            }
        }
    }
}