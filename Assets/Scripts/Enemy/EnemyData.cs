using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "Data/Enemy Data")]
    public class EnemyData : ScriptableObject
    {
        public float health = 100;
        public float attackPower = 1;
        public float attackRange = 1;
        public float damageRange = 1;
        public float movementSpeed = 10;
        public RuntimeAnimatorController animatorController;
        public EnemyName enemyName = EnemyName.SludgeGrunt;
        public int projectileCount = 1;
        public int maxExperienceDrop = 1;
    }

    public enum EnemyName
    {
        SludgeGrunt,
        SwiftSlimer,
        GooGuardian,
        SlimeSpitter,
        GlobLobber,
        GelGrenadier,
        BlastBlob,
        GoliathOoze
    }
}


