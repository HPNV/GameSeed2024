using System.Collections;
using System.Collections.Generic;
using Manager;
using Unity.VisualScripting;
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
        private EnemyData enemyData;

        private float currentHealth;    
    
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
            
            transform.position += direction * (enemyData.movementSpeed * Time.deltaTime);
    
            if (Input.GetKeyDown(KeyCode.A))
            {
                var newPos = transform.position;
                
                SpawnerManager.Spawn(SpawnType.ExperienceOrb, new Vector3(newPos.x, newPos.y, newPos.z));
                Damage(1);
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
            _healthBar = GetComponentInChildren<HealthBar>();
            
            _healthBar.gameObject.transform.SetParent(transform);
            _healthBar.gameObject.transform.localPosition = healthBarOffset;
            
            _healthBar.MaxHealth = enemyData.health;
        }
    
        public void Damage(float value)
        {
            _healthBar.Health -= value;
            currentHealth -= value;
            
            if (currentHealth <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            
        }
    }
}

