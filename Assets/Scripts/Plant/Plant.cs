using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Enemy;
using Plant.Factory;
using Plant.States;
using Script;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Plant
{
    public class Plant : Entity
    {
        public PlantData Data
        {
            get => _data;
            set
            {
                _data = value;
                AttackCooldown = _data.attackCooldown;
                InitDetector();
            }
        }
        [field: SerializeField]
        public Animator Animator { get; private set; }
        public TargetService TargetService { get; private set; }
        public EPlantState CurrentState => _states.FirstOrDefault(x => x.Value == _state).Key;
        public float AttackCooldown { get; set; }
        private PlantData _data;
        public float health;
        public float damage;
        public int level;
        private Dictionary<EPlantState, PlantState> _states; 
        private PlantState _state;
        private static readonly int GrowUp = Animator.StringToHash("GrowUp");

        private void Start()
        {
            Init();
        }

        private void Update()
        {
            _state.Update();
        }

        public void Init()
        {
            GetComponent<Animator>().runtimeAnimatorController = Data.animatorController;
            SpriteRenderer = GetComponent<SpriteRenderer>();

            if (!Data.hasCollider)
            {
                var collider2d = GetComponent<CircleCollider2D>();
                if (collider2d is not null)
                    Destroy(collider2d);
            }

            Health = _data.health;
            damage = _data.damage;
            level = _data.level;
            
            Init(_data.health, _data.health);
            InitState();
            InitTargetService();
        }

        private void InitState()
        {
            if (_state != null && _states != null) 
                return;

            _states = PlantStateFactory.CreateStates(Data.plantType, this);
            
            _state = _states[EPlantState.Idle];
        }

        private void InitTargetService()
        {
            switch (Data.targetType)
            {
                case TargetType.Single: 
                    TargetService = GetComponent<SingleTargetProvider>();
                    break;
                case TargetType.Multi:
                    TargetService = GetComponent<MultiTargetProvider>();
                    break;
            }
        }

        private void InitDetector()
        {
            var detector = transform.Find("Detector");
            detector.localScale = new Vector3(Data.range, Data.range, 1);
        }
        
        public override void Damage(float damage)
        {
            base.Damage(damage);
            
            if (CurrentState == EPlantState.Grow && Health <= _data.health - 5)
                ChangeState(EPlantState.Die);
        }

        public void ChangeState(EPlantState state)
        {
            if (_state == _states[state]) 
                return;
            
            _state.OnExit();
            _state = _states[state];
            _state.OnEnter();
        }
        
        public void Grow()
        {
            if (CurrentState != EPlantState.Grow)
                return;
                    
            ChangeState(EPlantState.Idle);
            Animator.SetTrigger(GrowUp);
        }

        protected override bool ValidateDamage()
        {
            return true;
        }

        protected override void OnDamage(float dmg)
        {
            StartCoroutine(Flash(Color.red));
            
            if(_data.plantType == EPlant.Raflessnare) PlayerManager.Instance.OnRafflesiaDamage(dmg);
        }

        protected override void OnHeal()
        {
            StartCoroutine(Flash(Color.green));
        }

        protected override void OnDie()
        {
            if (CurrentState != EPlantState.Die)
                ChangeState(EPlantState.Die);
        }

        protected override void OnSpeedUp()
        {
            Animator.speed += 0.5f;
            AttackCooldown -= 0.5f;
            StartCoroutine(Flash(Color.blue));
        }

        protected override void OnSpeedUpClear()
        {
            Animator.speed -= 0.5f;
            AttackCooldown += 0.5f;
        }


    }
}