using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enemy.States;
using Enemy.States.Explosive;
using Enemy.States.Melee;
using Enemy.States.Ranged;
using JetBrains.Annotations;
using Service;
using UnityEngine;

namespace Enemy
{
    public class EnemyBehaviour : MonoBehaviour
    {
        [SerializeField] 
        public EnemyData enemyData;
        public EnemyData baseData;
        public float CurrentHealth { get; private set; }
        public Animator Animator { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }
        public PlantTargetService PlantTargetService { get; private set; }
        public const string TargetTag = "Plant";
        
        private IState _currentState;
        private Dictionary<State, IState> _states;
        private Color _originalColor;
        
        public State CurrentState => _states.FirstOrDefault(x => x.Value == _currentState).Key;
        
        protected void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            Animator = GetComponent<Animator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            CurrentHealth = enemyData.health;
            PlantTargetService = GetComponentInChildren<PlantTargetService>(); 
            _originalColor = SpriteRenderer.color;
            
            SetupStates();
            SetupAnimationController();
        }

        protected void OnCollisionStay2D(Collision2D other)
        {
            _currentState.OnCollisionStay2D(other);
        }
        

        protected void Update()
        {
            _currentState.OnUpdate();
            
            if (Input.GetKeyDown(KeyCode.A))
            {
                Damage(10);
            }
            
                        
            if (CurrentHealth <= 0 && _currentState is not DieState)
            {
                ChangeState(State.Die);
            }
        }
        

        private void SetupStates()
        {
            _states = enemyData.enemyType switch
            {
                EnemyType.Melee => new Dictionary<State, IState>
                {
                    { State.Move, new MeleeMoveState(this) },
                    { State.Attack, new MeleeAttackState(this) },
                    { State.Die, new DieState(this) }
                },
                EnemyType.Ranged => new Dictionary<State, IState>
                {
                    { State.Move, new RangedMoveState(this) },
                    { State.Attack, new RangedAttackState(this) },
                    { State.Die, new DieState(this) }
                },
                EnemyType.Explosive => new Dictionary<State, IState>
                {
                    { State.Move, new ExplosiveMoveState(this) },
                    { State.Attack, new ExplosiveAttackState(this) },
                    { State.Die, new DieState(this) },
                    { State.Explode, new ExplosiveExplodeState(this) }
                },
                _ => _states
            };

            _currentState = _states[State.Move];
        }

        private void SetupAnimationController()
        {
            Animator.runtimeAnimatorController = enemyData.animatorController;
        }

        public void Damage(float value)
        {
            if(_currentState is DieState)
                return;
            
            CurrentHealth -= value;
            StartCoroutine(FlashRed());
        }

        public void ChangeState(State state)
        {
            _currentState.OnExit();
            _currentState = _states[state];
            _currentState.OnEnter();
        }
        
        private IEnumerator FlashRed()
        {
            SpriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            SpriteRenderer.color = _originalColor;
        }

        
    }
}

