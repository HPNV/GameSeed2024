using System;
using System.Collections;
using Manager;
using UnityEngine;
using Utils;

namespace PickupableResource
{
    public class ResourceBehaviour : MonoBehaviour
    {
        [SerializeField] 
        public ResourceData resourceData;
        private Coroutine _despawnCoroutine;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
            var spriteRenderer = GetComponent<SpriteRenderer>();
            
            _despawnCoroutine = StartCoroutine(Despawn());
            spriteRenderer.sprite = resourceData.sprite;
            transform.localScale = resourceData.scale;
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                var target = (Vector2)_camera.ScreenToWorldPoint(Input.mousePosition);
                
                var distance = Vector2.Distance(transform.position, target);

                if (distance < resourceData.pickupDistance)
                {
                    SingletonGame.Instance.ResourceManager.Pickup(this);
                    PlayerManager.Instance.OnResourceCollect(1);
                }

                SingletonGame.Instance.homeBase.UpdateUI();
            }
        }

        private void OnDestroy()
        {
            if (_despawnCoroutine != null)
                StopCoroutine(_despawnCoroutine);
        }

        private IEnumerator Despawn()
        {
            yield return new WaitForSeconds(10);
            SingletonGame.Instance.ResourceManager.Despawn(this);
        }
        
    }   
}
