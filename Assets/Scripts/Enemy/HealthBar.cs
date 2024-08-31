using UnityEngine;
using UnityEngine.UI;


namespace Enemy
{
    public class HealthBar : MonoBehaviour
    {
        private float _maxHealth = 100f;
        private Slider _healthSlider;
        private float _currentHealth;
    
    
        public float Health
        {
            get => _currentHealth;
            set
            {
                _currentHealth = value;
                _healthSlider.value = value;
            }
        }
        
        public float MaxHealth
        {
            get => _maxHealth;
            set
            {
                _maxHealth = value;
                _currentHealth = value;
    
                if (_healthSlider != null)
                {
                    _healthSlider.maxValue = _maxHealth;
                    _healthSlider.value = _maxHealth;
                }
            }
        }
    
        private void Start()
        {
            _healthSlider = GetComponentInChildren<Slider>();
            _healthSlider.maxValue = _maxHealth;
            _healthSlider.value = _maxHealth;
        }
        // Update is called once per frame
        private void Update()
        {
            KeepZValue();
        }
        
        private void KeepZValue()
        {
            var position = transform.position;
            position.z = 0;
            transform.position = position;
        }
        
    }
   
}