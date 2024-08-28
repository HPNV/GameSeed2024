using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyBehaviour : MonoBehaviour
    {
        [SerializeField]
        private Transform target;
        [SerializeField] 
        private Vector3 healthBarOffset = new(0, 1, 0);
        [SerializeField]
        private float moveSpeed = 0.005f;
    
        [SerializeField] 
        private GameObject healthBarObject;
    
        private HealthBar _healthBar;
        
      
    
        private void Start()
        {
            SetupHealthBar();
        }
    
        // Update is called once per frame
        private void Update()
        {
            KeepZValue();
            
            var targetPosition = target.position;
            
            var direction = (targetPosition - transform.position).normalized;
            
            transform.position += direction * (moveSpeed * Time.deltaTime);
    
            if (Input.GetKeyDown(KeyCode.A))
            {
                _healthBar.Health -= 1;
            }
        }
        
        private void KeepZValue()
        {
            var position = transform.position;
            position.z = 0;
            transform.position = position;
        }
    
        private void SetupHealthBar()
        {
            var healthBar = Instantiate(healthBarObject, transform);
            healthBar.transform.SetParent(transform);
            healthBar.transform.localPosition = healthBarOffset;
            
            _healthBar = healthBar.GetComponent<HealthBar>();
            _healthBar.MaxHealth = 100;
        }
    
        public void Damage(float value)
        {
            _healthBar.Health -= value;
        }
    }
}

