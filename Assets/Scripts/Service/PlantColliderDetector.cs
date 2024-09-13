using System.Collections.Generic;
using System.Linq;
using Enemy;
using UnityEngine;

namespace Service
{
    public class PlantColliderDetector : PlantDetectorService
    {

        private void PreparePlants()
        {
            Plants = Plants.OrderBy(plant => Vector2.Distance(plant.transform.position, transform.position)).ToList();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Plant"))
            {
                var plant = other.GetComponent<Plant.Plant>();

                if (plant != null && !plant.Data.hasCollider)
                    return;
                
                Plants.Add(other.gameObject);
                PreparePlants();
            }

            if(other.CompareTag("Base")) {
                Plants.Add(other.gameObject);
                PreparePlants();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Plant"))
            {
                Plants.Remove(other.gameObject);
                PreparePlants();
            }
        }
    }
}