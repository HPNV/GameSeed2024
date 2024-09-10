using Enemy;
using Projectile.Behaviour;
using UnityEngine;

namespace Projectile.Behaviour
{
    public class EnemyProjectileBehaviour : ProjectileBaseBehaviour
    {
        public EnemyProjectileBehaviour(Projectile projectile) : base(projectile){}
        
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
            var entity = collider.gameObject.GetComponent<Entity>();

            if (entity is null)
                return;
            
            entity.Damage(Projectile.AttackPower);
            SingletonGame.Instance.ProjectileManager.Despawn(Projectile);
        }
        
    }
}