using System.Collections.Generic;
using UnityEngine;


namespace Service
{
    public abstract class PlantDetectorService : MonoBehaviour
    {
        protected List<GameObject> Plants = new();

        public List<GameObject> GetPlantsInRange()
        {
            return Plants;
        }
    }
}