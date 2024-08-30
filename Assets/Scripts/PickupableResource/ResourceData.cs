using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PickupableResource
{
    [CreateAssetMenu(fileName = "New Resource", menuName = "Data/Resource Data")]
    public class ResourceData : ScriptableObject
    {
        public string resourceName;

        public float pickupDistance;

        public Sprite sprite;

        public float dropChance;
        //TODO ADD RESOURCE ASSETS
    }
}

