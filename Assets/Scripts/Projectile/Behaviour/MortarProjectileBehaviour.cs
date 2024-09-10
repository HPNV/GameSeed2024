using System;
using Enemy;
using Enemy.States;
using UnityEngine;

namespace Projectile.Behaviour
{
    public class MortarProjectileBehaviour : ProjectileBaseBehaviour
    {
        private EnemyBehaviour _attackEnemy;
        private readonly float _inAirTime;
        private readonly float _speed;
        private readonly float _initialSpeedSprite;
        private float _timeAlive;
        private const float Gravity = 10f;

        public MortarProjectileBehaviour(Projectile projectile) : base(projectile)
        {
            _inAirTime = 2f;
            var totalDistance = Vector2.Distance(Projectile.transform.position, Projectile.Target);
            _speed = totalDistance / _inAirTime;
            _initialSpeedSprite = 10;
        }

        
        public override void Update()
        {
            var distanceToMove = _speed * Time.deltaTime;
           
            Projectile.transform.position = Vector2.MoveTowards(
                Projectile.transform.position,
                Projectile.Target,
                distanceToMove
            );

            _timeAlive += Time.deltaTime;
            
            var ySprite = _initialSpeedSprite * _timeAlive - Gravity * _timeAlive * _timeAlive / 2;
            
            
            Projectile.SpriteObject.transform.localPosition = new Vector3(0, Math.Max(ySprite, 0), 0);
            
            if (_timeAlive >= _inAirTime && _attackEnemy != null)
            {
                var distance = Vector2.Distance(Projectile.transform.position, _attackEnemy.transform.position);

                if (distance <= Projectile.data.attackRadius)
                {
                    _attackEnemy.Damage(Projectile.AttackPower);
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
            _attackEnemy = enemy;
        }
    }
}