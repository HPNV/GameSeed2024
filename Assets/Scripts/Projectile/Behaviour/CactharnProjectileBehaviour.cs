using Enemy;
using UnityEngine;

namespace Projectile.Behaviour
{
    public class CactharnProjectileBehaviour : ProjectileBaseBehaviour
    {
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
            
            enemy.Damage(Projectile.data.attackPower);
            SingletonGame.Instance.ProjectileManager.Despawn(Projectile);
        }
    }
}