using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

namespace PickupableResource
{
    [CreateAssetMenu(fileName = "New Resource", menuName = "Data/Resource Data")]
    public class ResourceData : ScriptableObject
    {
        public ResourceType resourceName;
        public float pickupDistance;
        public Sprite sprite;
        public float dropChance;
        public Vector3 scale = new(2, 2, 2);
    }
}

