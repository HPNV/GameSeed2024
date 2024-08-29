using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public int health;
    public float attackPower;
    public float movementSpeed;

    public int Experience
    {
        get => (int)(health * attackPower * movementSpeed / 100);
    }
}
