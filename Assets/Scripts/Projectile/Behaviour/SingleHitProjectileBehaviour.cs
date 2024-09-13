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

        public override void OnCollide(Collider2D collider)
        {
            var enemy = collider.gameObject.GetComponent<EnemyBehaviour>();
            
            if (enemy is null)
                return;
            
            if (enemy.CurrentState == State.Die)
                return;
            
            enemy.Damage(Projectile.AttackPower);
            
            SingletonGame.Instance.ProjectileManager.Despawn(Projectile);
        }
    }
}