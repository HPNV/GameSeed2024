using System.Collections;
using System.Collections.Generic;
using Enemy.States;
using Manager;
using UnityEngine;

namespace Enemy
{
    public class EnemyBehaviour : MonoBehaviour
    {
        [SerializeField]
        public Transform target;
        [SerializeField] 
        private Vector3 healthBarOffset = new(0, 1, 0);

        [SerializeField] 
        public EnemyData enemyData;

        private float _currentHealth;    
    
        private HealthBar _healthBar;
        
        private IState _currentState;
        private Dictionary<State, IState> _states;
      
    
        private void Start()
        { 
            _states = new Dictionary<State, IState>
            {
                {State.Move, new MoveState()},
                {State.Attack, new AttackState()},
                {State.Die, new DieState()}
            };
            _currentState = _states[State.Move];
            _currentHealth = enemyData.health;
            SetupHealthBar();
        }
    
        // Update is called once per frame
        private void Update()
        {
            KeepZValue();
            _currentState.OnUpdate(this);
    
            if (Input.GetKeyDown(KeyCode.A))
            {
                Damage(10);
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
            _currentHealth -= value;
            
            if (_currentHealth <= 0)
            {
                Debug.Log($"CurrentHealth: {_currentHealth}");
                ChangeState(State.Die);
            }
        }

        public void ChangeState(State state)
        {
            _currentState.OnExit(this);
            _currentState = _states[state];
            _currentState.OnEnter(this);
        }
    }
}

