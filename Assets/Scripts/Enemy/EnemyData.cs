using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "Data/Enemy Data")]
    public class EnemyData : ScriptableObject
    {
        public string enemyName;
        public int health;
        public float attackPower;
        public float movementSpeed;
        public EnemyType enemyType;

        public int Experience
        {
            get => (int)(health * attackPower * movementSpeed / 100);
        }
    }

    public enum EnemyType
    {
        Melee,
        Ranged,
    }
}


