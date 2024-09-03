using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Service
{
    public abstract class PlantTargetService : MonoBehaviour
    {
        [SerializeField] 
        protected Transform currentPlant;
        [SerializeField]
        protected PlantDetectorService plantDetectorService;

        [CanBeNull] public abstract GameObject GetTarget();
    }
}