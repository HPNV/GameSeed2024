using System;
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
        private Vector3 healthBarOffset = new(0, 1, 0);

        [SerializeField] 
        public EnemyData enemyData;
        public float CurrentHealth { get; set; }
        public Transform Target { get; set; }
        public Animator Animator { get; set; }
        public SpriteRenderer SpriteRenderer { get; set; }
        
        private HealthBar _healthBar;
        private IState _currentState;
        private Dictionary<State, IState> _states;
      
    
        private void Start()
        {
            Target = GameObject.FindWithTag("Dummy").transform;
            Animator = GetComponent<Animator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            CurrentHealth = enemyData.health;
            SetupHealthBar();
            SetupStates();
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            _currentState.OnCollisionStay2D(other);
        }

        // Update is called once per frame
        private void Update()
        {
            _currentState.OnUpdate();
    
            
            if (Input.GetKeyDown(KeyCode.A))
            {
                Damage(10);
            }
        }
        
    
        private void SetupHealthBar()
        {
            _healthBar = GetComponentInChildren<HealthBar>();
            
            _healthBar.gameObject.transform.SetParent(transform);
            _healthBar.gameObject.transform.localPosition = healthBarOffset;
            // _healthBar.gameObject.transform.localScale = new Vector3(1, 1, 1);
            _healthBar.MaxHealth = enemyData.health;
        }

        private void SetupStates()
        {
            _states = new Dictionary<State, IState>
            {
                {State.Move, new MoveState(this)},
                {State.Die, new DieState(this)}
            };

            switch (enemyData.enemyType)
            {
                case EnemyType.Melee:
                    _states.Add(State.Attack, new MeleeAttackState(this));
                    break;
                case EnemyType.Ranged:
                    _states.Add(State.Attack, new MeleeAttackState(this));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            _currentState = _states[State.Move];
        }
    
        public void Damage(float value)
        {
            _healthBar.Health -= value;
            CurrentHealth -= value;
        }

        public void ChangeState(State state)
        {
            _currentState.OnExit();
            _currentState = _states[state];
            _currentState.OnEnter();
        }
    }
}

