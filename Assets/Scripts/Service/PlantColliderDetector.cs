using System.Collections.Generic;
using System.Linq;
using Enemy;
using UnityEngine;

namespace Service
{
    public class PlantColliderDetector : PlantDetectorService
    {

        private void SortPlants()
        {
            Plants = Plants.OrderBy(plant => Vector2.Distance(plant.transform.position, transform.position)).ToList();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            //TODO CHANGE TO GETCOMPONENT
            if (other.CompareTag("Plant"))
            {
                Plants.Add(other.gameObject);
                SortPlants();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Plant"))
            {
                Plants.Remove(other.gameObject);
                SortPlants();
            }
        }
    }
}