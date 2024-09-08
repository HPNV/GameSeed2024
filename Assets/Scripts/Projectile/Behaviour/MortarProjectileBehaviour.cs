using System;
using System.Collections.Generic;
using Enemy;
using Enemy.States;
using UnityEngine;

namespace Projectile.Behaviour
{
    public class MortarProjectileBehaviour : ProjectileBaseBehaviour
    {
        private EnemyBehaviour _attackEnemy;
        private float _inAirTime;
        private float _totalDistance;
        private float _speed;
        private float _initialSpeedSprite;
        private float _timeAlive;
        private float _gravity = 10f;

        public MortarProjectileBehaviour(Projectile projectile) : base(projectile)
        {
            _inAirTime = 2f;
            _totalDistance = Vector2.Distance(Projectile.transform.position, Projectile.Target);
            _speed = _totalDistance / _inAirTime;
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
            
            var ySprite = _initialSpeedSprite * _timeAlive - _gravity * _timeAlive * _timeAlive / 2;
            
            
            
            Projectile.SpriteObject.transform.localPosition = new Vector3(0, Math.Max(ySprite, 0), 0);
            
            if (_timeAlive >= _inAirTime && _attackEnemy != null)
            {
                var distance = Vector2.Distance(Projectile.transform.position, _attackEnemy.transform.position);

                if (distance <= 0.5f)
                {
                    _attackEnemy.Damage(Projectile.data.attackPower);
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