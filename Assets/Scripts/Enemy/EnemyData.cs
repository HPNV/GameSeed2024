using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "Data/Enemy Data")]
    public class EnemyData : ScriptableObject
    {
        public string enemyName = "Dep";
        public float health = 100;
        public float attackPower = 1;
        public float attackRange = 1;
        public float damageRange = 1;
        public float movementSpeed = 10;
        public RuntimeAnimatorController animatorController;
        public EnemyType enemyType = EnemyType.Melee;
        public int projectileCount = 1;

        public int Experience
        {
            get => (int)(health * attackPower * movementSpeed / 100);
        }
    }

    public enum EnemyType
    {
        Melee,
        MeleeFast,
        MeleeStrong,
        Ranged,
        RangedTwo,
        RangedThree,
        Explosive,
        Large
    }
}


