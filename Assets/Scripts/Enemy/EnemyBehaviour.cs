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
        public float CurrentHealth { get; set; }
        public Transform Target { get; set; }
        public Animator Animator { get; set; }
        public SpriteRenderer SpriteRenderer { get; set; }
        
        protected HealthBar HealthBar;
        protected IState CurrentState;
        protected Dictionary<State, IState> States;
      
    
        protected void Start()
        {
            Target = GameObject.FindWithTag("Dummy").transform;
            Animator = GetComponent<Animator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            CurrentHealth = enemyData.health;
            SetupHealthBar();
            SetupStates();
            SetupAnimationController();
        }

        protected void OnCollisionStay2D(Collision2D other)
        {
            CurrentState.OnCollisionStay2D(other);
        }

        // Update is called once per frame
        protected void Update()
        {
            CurrentState.OnUpdate();
            
            if (Input.GetKeyDown(KeyCode.A))
            {
                Damage(10);
            }
        }
        
    
        private void SetupHealthBar()
        {
            HealthBar = GetComponentInChildren<HealthBar>();
            
            HealthBar.gameObject.transform.SetParent(transform);
            HealthBar.gameObject.transform.localPosition = healthBarOffset;
            // _healthBar.gameObject.transform.localScale = new Vector3(1, 1, 1);
            HealthBar.MaxHealth = enemyData.health;
        }

        private void SetupStates()
        {
            States = enemyData.enemyType switch
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
                _ => States
            };

            CurrentState = States[State.Move];
        }

        private void SetupAnimationController()
        {
            Animator.runtimeAnimatorController = enemyData.animatorController;
        }

        private void Damage(float value)
        {
            HealthBar.Health -= value;
            CurrentHealth -= value;
        }

        public void ChangeState(State state)
        {
            CurrentState.OnExit();
            CurrentState = States[state];
            CurrentState.OnEnter();
        }
    }
}

