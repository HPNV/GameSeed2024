using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PickupableResource
{
    public class ResourceBehaviour : MonoBehaviour
    {
        [SerializeField] 
        public ResourceData resourceData;
        public Vector3 Target { get; set; }
        
        private Camera _camera;
        // Start is called before the first frame update
        private void Start()
        {
            _camera = Camera.main;
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = resourceData.sprite;
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                var target = (Vector2)_camera.ScreenToWorldPoint(Input.mousePosition);
                
                Debug.Log($"{Vector3.Distance(transform.position, target)} {resourceData.pickupDistance}");
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
