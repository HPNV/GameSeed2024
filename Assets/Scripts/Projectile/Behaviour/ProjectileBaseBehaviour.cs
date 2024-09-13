using Unity.Profiling;
using UnityEngine;

namespace Projectile.Behaviour
{
    public abstract class ProjectileBaseBehaviour : IProjectileBehaviour
    {
        protected Projectile Projectile;
        public ProjectileBaseBehaviour(Projectile projectile)
        {
            Projectile = projectile;
        }
        public virtual void Update() {}

        public virtual void OnCollide(Collider2D collider) {}
        
        public virtual void OnDespawn() {}
    }
}