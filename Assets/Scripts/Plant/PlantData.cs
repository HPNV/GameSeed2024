using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "New Plant", menuName = "Plant/Plant Data")]
public class PlantData : ScriptableObject
{
    public string plantName;
    public float health;
    public float damage;
    public float speed;
    public float range;

    public AnimatorController animatorController;
    
    public TargetType targetType;
}