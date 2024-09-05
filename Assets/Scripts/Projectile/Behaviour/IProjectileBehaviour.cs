using UnityEngine;

namespace Projectile.Behaviour
{
    public interface IProjectileBehaviour
    {
        public void Move();
        public void OnCollide(Collision2D collider);
    }
}