using System.Collections.Generic;
using Enemy;
using Enemy.States;
using Unity.VisualScripting;
using UnityEngine;

namespace Projectile.Behaviour
{
    public class KnockbackProjectileBehaviour : ProjectileBaseBehaviour
    {
        private Rigidbody2D _rigidBody2D;
        private BoxCollider2D _boxCollider2D;
        private CircleCollider2D _circleCollider2D;
        public KnockbackProjectileBehaviour(Projectile projectile) : base(projectile)
        {
            _circleCollider2D = projectile.GetComponent<CircleCollider2D>();
            _boxCollider2D = projectile.AddComponent<BoxCollider2D>();
            
            _boxCollider2D.excludeLayers = ~LayerMask.GetMask(projectile.data.targetLayer);
            _boxCollider2D.size = new Vector2(0.5f, 1f);
            _circleCollider2D.enabled = false;

            _rigidBody2D = projectile.GetComponent<Rigidbody2D>();
            _rigidBody2D.bodyType = RigidbodyType2D.Kinematic;
        }
        
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
            
        }
        
        public override void OnDespawn()
        {
            _rigidBody2D.bodyType = RigidbodyType2D.Dynamic;
            var boxCollider2D = Projectile.GetComponent<BoxCollider2D>();
            
            Debug.Log($"COLLIDER {boxCollider2D}");
            Object.Destroy(boxCollider2D); 
            _circleCollider2D.enabled = true;
        }
    }
}