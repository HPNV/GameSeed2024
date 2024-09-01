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
                var target = (Vector2)_camera.ScreenToWorldPoint(Input.mousePosition);
                
                if (Vector3.Distance(transform.position, target) < resourceData.pickupDistance)
                {
                    SingletonGame.Instance.ExperienceManager.Spawn(1, transform.position);
                    Destroy();
                }
            }
        }
        
        private void Destroy()
        {
            Destroy(gameObject);
        }
    }   
}
