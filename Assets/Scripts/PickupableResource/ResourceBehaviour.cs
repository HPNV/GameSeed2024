using Manager;
using UnityEngine;
using Utils;

namespace PickupableResource
{
    public class ResourceBehaviour : MonoBehaviour
    {
        [SerializeField] 
        public ResourceData resourceData;
        
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = resourceData.sprite;
            transform.localScale = resourceData.scale;
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Debug.Log("Mouse0 pressed");
                var target = (Vector2)_camera.ScreenToWorldPoint(Input.mousePosition);
                Debug.Log($"Mouse Position: {target}, Object Position: {transform.position}, Pickup Distance: {resourceData.pickupDistance}");
                
                float distance = Vector2.Distance(transform.position, target);
                Debug.Log($"Calculated Distance: {distance}");

                if (distance < resourceData.pickupDistance)
                {
                    Debug.Log("Mouse0 clicked");
                    if(resourceData.resourceName == ResourceType.Water)
                    {
                        SingletonGame.Instance.homeBase.addWater(1);
                    }
                    else if(resourceData.resourceName == ResourceType.Sunlight)
                    {
                        SingletonGame.Instance.homeBase.addSun(1);
                    }
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("Distance too far for pickup");
                }
                SingletonGame.Instance.homeBase.UpdatetUI();
            }
        }
        
        private void Destroy()
        {
            Destroy(gameObject);
        }
    }   
}
