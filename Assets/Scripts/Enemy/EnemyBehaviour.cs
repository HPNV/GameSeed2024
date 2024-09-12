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
    public class EnemyBehaviour : Entity
    {
        [SerializeField] 
        public EnemyData enemyData;
        public EnemyData baseData;
        public Animator Animator { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }
        public PlantTargetService PlantTargetService { get; private set; }
        public CircleCollider2D InnerCircleCollider { get; private set; }
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
            PlantTargetService = GetComponentInChildren<PlantTargetService>(); 
            InnerCircleCollider = GetComponent<CircleCollider2D>();
            _originalColor = SpriteRenderer.color;
            
            InnerCircleCollider.enabled = true;
            Init(enemyData.health, enemyData.health);
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
            
                        
            // if (CurrentHealth <= 0 && _currentState is not DieState)
            // {
            //     ChangeState(State.Die);
            // }
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
                EnemyType.MeleeStrong => new Dictionary<State, IState>
                {
                    { State.Move, new MeleeMoveState(this) },
                    { State.Attack, new MeleeAttackState(this) },
                    { State.Die, new DieState(this) }
                },
                EnemyType.MeleeFast => new Dictionary<State, IState>
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
                EnemyType.RangedTwo => new Dictionary<State, IState>
                {
                    { State.Move, new RangedMoveState(this) },
                    { State.Attack, new RangedAttackState(this) },
                    { State.Die, new DieState(this) }
                },
                EnemyType.RangedThree => new Dictionary<State, IState>
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

        protected override bool ValidateDamage()
        {
            return _currentState is not DieState;
        }

        protected override void OnDamage()
        {
            StartCoroutine(FlashRed());
        }

        protected override void OnHeal()
        {
        }

        protected override void OnDie()
        {
            if (_currentState is not DieState)
                ChangeState(State.Die);
        }

        protected override void OnSpeedUp()
        {
            Animator.speed += 0.05f;
        }

        protected override void OnSpeedUpClear()
        {
            Animator.speed -= 0.05f;
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

