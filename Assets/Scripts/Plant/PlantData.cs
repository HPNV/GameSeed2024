using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;


namespace Plant
{
    [CreateAssetMenu(fileName = "New Plant", menuName = "Plant/Plant Data")]
    public class PlantData : ScriptableObject
    {
        public EPlant plantType;
        public float health;
        public float damage;
        public int attackCooldown;
        public float range;
        public string description;
        public bool hasCollider = true;

        public Sprite sprite;
        public RuntimeAnimatorController animatorController;

        public TargetType targetType;
    }
    
}