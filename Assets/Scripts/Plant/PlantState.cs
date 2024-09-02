using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Plant
{
    public abstract class PlantState
    {
        protected Plant Plant;

        protected PlantState(Plant plant)
        {
            Plant = plant;
        }

        public abstract void Update();
    }
}