using System.Collections.Generic;
using Enemy.States;
using Enemy.States.Explosive;
using Enemy.States.Melee;
using Enemy.States.Ranged;
using UnityEngine;

namespace Enemy
{
    public class EnemyBehaviour : MonoBehaviour
    {
        [SerializeField] 
        private Vector3 healthBarOffset = new(0, 1, 0);

        [SerializeField] 
        public EnemyData enemyData;
        public float CurrentHealth { get; private set; }
        public Transform Target { get; private set; }
        public Animator Animator { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }

        private HealthBar _healthBar;
        private IState _currentState;
        private Dictionary<State, IState> _states;
      
    
        protected void Start()
        {
            Target = GameObject.FindWithTag("Base").transform;
            Animator = GetComponent<Animator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            CurrentHealth = enemyData.health;
            SetupHealthBar();
            SetupStates();
            SetupAnimationController();
        }

        protected void OnCollisionStay2D(Collision2D other)
        {
            _currentState.OnCollisionStay2D(other);
        }

        // Update is called once per frame
        protected void Update()
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

            if (_healthBar == null)
                return;

            var healthObject = _healthBar.gameObject;
            
            healthObject.transform.SetParent(transform);
            healthObject.transform.localPosition = healthBarOffset;
            // _healthBar.gameObject.transform.localScale = new Vector3(1, 1, 1);
            _healthBar.MaxHealth = enemyData.health;
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
                    { State.Die, new DieState(this) }
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

