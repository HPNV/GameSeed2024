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
            var plant = collider.gameObject.GetComponent<Plant.Plant>();

            if (plant is null)
                return;
            
            plant.Damage(Projectile.AttackPower);
            SingletonGame.Instance.ProjectileManager.Despawn(Projectile);
        }
        
    }
}