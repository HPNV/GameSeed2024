using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace Projectile.Behaviour
{
    public class CactharnProjectileBehaviour : ProjectileBaseBehaviour
    {
        private HashSet<EnemyBehaviour> _enemiesHit = new ();
        public CactharnProjectileBehaviour(Projectile projectile) : base(projectile){}
        
        public override void Move()
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
            
            if (_enemiesHit.Contains(enemy))
                return;
            
            Physics2D.IgnoreCollision(Projectile.GetComponent<Collider2D>(), collider.collider);
            _enemiesHit.Add(enemy);
            
            enemy.Damage(Projectile.data.attackPower);
        }
    }
}