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
        public string plantName;
        public float health;
        public float damage;
        public int cd;
        public float range;
        public string description;
<<<<<<< Updated upstream
=======
        public int level;
        public bool hasCollider = true;
>>>>>>> Stashed changes

        public Sprite sprite;
        public AnimatorController animatorController;
    
        public TargetType targetType;
    }
}