using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Plant", menuName = "Plant/Plant Data")]
public class PlantData : ScriptableObject
{
    public string plantName;
    public float health;
    public float damage;
    public float speed;
}
