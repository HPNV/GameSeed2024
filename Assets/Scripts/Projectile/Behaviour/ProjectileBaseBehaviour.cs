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
        public virtual void Move() {}

        public virtual void OnCollide(Collision2D collider) {}
    }
}