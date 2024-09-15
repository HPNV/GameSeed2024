using System.Collections.Generic;
using Enemy;
using Plant;
using UnityEngine;

namespace Projectile.Behaviour
{
    public class SpeedingProjectileBehaviour : ProjectileBaseBehaviour
    {
        private readonly Rigidbody2D _rigidbody2D;
        private readonly HashSet<Plant.Plant> _plantsHit = new ();

        public SpeedingProjectileBehaviour(Projectile projectile) : base(projectile)
        {
            _rigidbody2D = Projectile.GetComponent<Rigidbody2D>();
            _rigidbody2D.constraints = RigidbodyConstraints2D.FreezePosition;
            
            var circleCollider2D = Projectile.GetComponent<CircleCollider2D>();
            
            circleCollider2D.radius = 0.5f;
        }
        
        
        public override void Update()
        {
            Projectile.transform.localScale += new Vector3(1, 1, 0) * (Projectile.data.movementSpeed * Time.deltaTime);
            
            var spriteRenderer = Projectile.GetComponentInChildren<SpriteRenderer>();
            
            if (spriteRenderer is null)
                return;
            
            var color = spriteRenderer.color;
            color.a -= Time.deltaTime / 3;
            spriteRenderer.color = color;
        }

        public override void OnCollide(Collider2D collider)
        {
            var plant = collider.gameObject.GetComponent<Plant.Plant>();
            
            if (plant is null)
                return;
            
            if (_plantsHit.Contains(plant))
                return;
            
            _plantsHit.Add(plant);

            if (plant.Data.plantType == EPlant.Swiftglory)
                return;
            
            plant.SpeedUp(Projectile.AttackPower);
        }

        public override void OnDespawn()
        {
            base.OnDespawn();
            _rigidbody2D.constraints = RigidbodyConstraints2D.None;
        }
    }
}