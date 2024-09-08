using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

namespace Projectile
{
    [CreateAssetMenu(fileName = "New Projectile", menuName = "Data/Projectile Data")]
    public class ProjectileData : ScriptableObject
    {
        public ProjectileType projectileType;
        public float attackPower = 1;
        public float movementSpeed = 10;
        public float attackRadius = 0.5f;
        public int lifetime = 100;
        public Sprite sprite;
        public float scale = 1;
        public string targetTag;
        public string targetLayer;
        [CanBeNull] public ParticleSystem particles;
    }
    
    public enum ProjectileType
    {
        Enemy,
        SingleHit,
        Piercing,
        Mortar,
        Healing,
        ExplosiveMortar
    }
}