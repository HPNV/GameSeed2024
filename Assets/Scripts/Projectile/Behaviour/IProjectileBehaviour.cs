using UnityEngine;

namespace Projectile.Behaviour
{
    public interface IProjectileBehaviour
    {
        public void Update();
        public void OnCollide(Collision2D collider);
    }
}