using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEditor.Animations;
using UnityEngine;


namespace Plant
{
    [CreateAssetMenu(fileName = "New Plant", menuName = "Plant/Plant Data")]
    public class PlantData : ScriptableObject
    {
        public PlantType plantType;
        public float health;
        public float damage;
        public int attackCooldown;
        public float range;
        public string description;

        public Sprite sprite;
        public AnimatorController animatorController;

        public TargetType targetType;
    }

    public enum PlantType
    {
        Cactharn,
        Boomkin,
        CobCorn,
        Raflessia,
        Weisshooter
    }
}