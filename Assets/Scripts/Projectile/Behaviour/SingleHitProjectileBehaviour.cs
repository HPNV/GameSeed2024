using System.Collections.Generic;
using Enemy;
using Enemy.States;
using UnityEngine;

namespace Projectile.Behaviour
{
    public class SingleHitProjectileBehaviour : ProjectileBaseBehaviour
    {
        public SingleHitProjectileBehaviour(Projectile projectile) : base(projectile){}
        
        public override void Update()
        {
            var currentPosition = Projectile.transform.position;
            
            Projectile.transform.position = Vector2.MoveTowards(
                currentPosition,
                (Vector2)currentPosition + Projectile.Direction,  
                Projectile.data.movementSpeed * Time.deltaTime);
        }

        public override void OnCollide(Collision2D collider)
        {
            var enemy = collider.gameObject.GetComponent<EnemyBehaviour>();
            
            if (enemy is null)
                return;
            
            if (enemy.CurrentState == State.Die)
                return;
            
            Physics2D.IgnoreCollision(Projectile.GetComponent<Collider2D>(), collider.collider);
            enemy.Damage(Projectile.AttackPower);
            
            SingletonGame.Instance.ProjectileManager.Despawn(Projectile);
        }
    }
}