using UnityEngine;

namespace Projectile.Behaviour
{
    public interface IProjectileBehaviour
    {
        public void Update();
        public void OnCollide(Collider2D collider);
        public void OnDespawn();
    }
}