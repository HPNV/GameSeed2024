using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace Projectile.Behaviour
{
    public class HealingProjectileBehaviour : ProjectileBaseBehaviour
    {
        private HashSet<Plant.Plant> _plantsHit = new ();
        public HealingProjectileBehaviour(Projectile projectile) : base(projectile){}
        
        
        public override void Update()
        {
            Projectile.transform.localScale += new Vector3(1, 1, 0) * (Projectile.data.movementSpeed * Time.deltaTime);
            
            var spriteRenderer = Projectile.GetComponentInChildren<SpriteRenderer>();
            
            if (spriteRenderer is null)
                return;
            
            var color = spriteRenderer.color;
            color.a -= Time.deltaTime / 2;
            spriteRenderer.color = color;
        }

        public override void OnCollide(Collision2D collider)
        {
            var plant = collider.gameObject.GetComponent<Plant.Plant>();
            
            if (plant is null)
                return;
            
            if (_plantsHit.Contains(plant))
                return;
            
            Physics2D.IgnoreCollision(Projectile.GetComponent<Collider2D>(), collider.collider);
            _plantsHit.Add(plant);

            if (plant.Data.plantType == EPlant.Aloecure)
                return;
            
            plant.Heal(Projectile.AttackPower);
        }
    }
}