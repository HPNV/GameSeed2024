using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "Data/Enemy Data")]
    public class EnemyData : ScriptableObject
    {
        public string enemyName = "Dep";
        public int health = 100;
        public float attackPower = 1;
        public float attackRange = 1;
        public float movementSpeed = 10;
        public RuntimeAnimatorController animatorController;
        public EnemyType enemyType = EnemyType.Melee;

        public int Experience
        {
            get => (int)(health * attackPower * movementSpeed / 100);
        }
    }

    public enum EnemyType
    {
        Melee,
        Ranged,
        Explosive
    }
}


