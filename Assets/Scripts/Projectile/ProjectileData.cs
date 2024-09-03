using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

namespace Projectile
{
    [CreateAssetMenu(fileName = "New Projectile", menuName = "Data/Projectile Data")]
    public class ProjectileData : ScriptableObject
    {
        public string projectileName = "Dep";
        public float attackPower = 1;
        public float movementSpeed = 10;
        public int lifetime = 100;
        public Sprite sprite;
        public float scale = 1;
        public ParticleSystem particles;
    }
    
    public enum ProjectileType
    {
        EnemyRanged,
    }
}