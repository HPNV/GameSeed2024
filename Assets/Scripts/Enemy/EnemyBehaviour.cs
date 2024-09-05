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
        public float CurrentHealth { get; private set; }
        public Animator Animator { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }
        public PlantTargetService PlantTargetService { get; private set; }

        public const string TargetTag = "Plant";
        private IState _currentState;
        private Dictionary<State, IState> _states;
        
        protected void Start()
        {
            Animator = GetComponent<Animator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            CurrentHealth = enemyData.health;
            PlantTargetService = GetComponentInChildren<PlantTargetService>();
            
            
            SetupStates();
            SetupAnimationController();
        }

        protected void OnCollisionStay2D(Collision2D other)
        {
            _currentState.OnCollisionStay2D(other);
        }
        
        protected void Update()
        {
            Debug.Log(PlantTargetService);
            Debug.Log(PlantTargetService.GetTarget());
            _currentState.OnUpdate();
            
            if (Input.GetKeyDown(KeyCode.A))
            {
                Damage(10);
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

        private void Damage(float value)
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
            var originalColor = SpriteRenderer.color;
            SpriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            SpriteRenderer.color = originalColor;
        }

        public void Reset()
        {
            CurrentHealth = enemyData.health;
            SetupStates();
            SetupAnimationController();
            ChangeState(State.Move);
        }
    }
}

