using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Plant.States.Aloecure;
using Plant.States.Bamburst;
using Plant.States.Boomkin;
using Plant.States.Cactharn;
using Plant.States.Cobcorn;
using Plant.States.Duricane;
using Plant.States.Weisshooter;
using Script;
using UnityEngine;

namespace Plant
{
    public class Plant : MonoBehaviour
    {
        public PlantData Data
        {
            get => _data;
            set
            {
                _data = value;
                InitDetector();
            }
        }
        [field: SerializeField]
        public Animator Animator { get; private set; }
        public TargetService TargetService { get; private set; }
        public EPlantState CurrentState => _states.FirstOrDefault(x => x.Value == _state).Key;
    
        private PlantData _data;
        private Dictionary<EPlantState, PlantState> _states; 
        private PlantState _state;
        private SpriteRenderer _spriteRenderer;
        
        private float _currentHealth;
        
        
        private void Start()
        {
            Init();
        }

        private void Update()
        {
            if (_currentHealth <= 0 && CurrentState != EPlantState.Die)
                ChangeState(EPlantState.Die);
            
            _state.Update();
        }

        public void Init()
        {
            GetComponent<Animator>().runtimeAnimatorController = Data.animatorController;
            _currentHealth = _data.health;
            _spriteRenderer = GetComponent<SpriteRenderer>();

            if (!Data.hasCollider)
            {
                var collider2d = GetComponent<CircleCollider2D>();
                if (collider2d is not null)
                    Destroy(collider2d);
            }
            
            InitState();
            InitTargetService();
        }

        private void InitState()
        {
            if (_state != null && _states != null) 
                return;
            
            _states = _data.plantType switch
            {
                EPlant.Cactharn => new Dictionary<EPlantState, PlantState>
                {
                    { EPlantState.Idle , new PlantIdleState(this)},
                    { EPlantState.Attack , new CactharnAttackState(this)},
                    { EPlantState.Select , new PlantSelectState(this)},
                    { EPlantState.Die, new PlantDieState(this)}
                },
                EPlant.Cobcorn => new Dictionary<EPlantState, PlantState>
                {
                    { EPlantState.Idle , new PlantIdleState(this)},
                    { EPlantState.Attack , new CobcornAttackState(this)},
                    { EPlantState.Select , new PlantSelectState(this)},
                    { EPlantState.Die, new PlantDieState(this)}
                },
                EPlant.Weisshooter => new Dictionary<EPlantState, PlantState>
                {
                    { EPlantState.Idle , new PlantIdleState(this)},
                    { EPlantState.Attack , new WeisshooterAttackState(this)},
                    { EPlantState.Select , new PlantSelectState(this)},
                    { EPlantState.Die, new PlantDieState(this)}
                },
                EPlant.Duricane => new Dictionary<EPlantState, PlantState>
                {
                    { EPlantState.Idle , new PlantIdleState(this)},
                    { EPlantState.Attack , new DuricaneAttackState(this)},
                    { EPlantState.Select , new PlantSelectState(this)},
                    { EPlantState.Die, new PlantDieState(this)}
                },
                EPlant.Boomkin => new Dictionary<EPlantState, PlantState>
                {
                    { EPlantState.Idle , new BoomkinIdleState(this)},
                    { EPlantState.Select , new PlantSelectState(this)},
                    { EPlantState.Die, new BoomkinDieState(this)}
                },
                EPlant.Bamburst => new Dictionary<EPlantState, PlantState>
                {
                    { EPlantState.Idle , new PlantIdleState(this)},
                    { EPlantState.Attack , new BamburstAttackState(this)},
                    { EPlantState.Select , new PlantSelectState(this)},
                    { EPlantState.Die, new PlantDieState(this)}
                },
                EPlant.Aloecure => new Dictionary<EPlantState, PlantState>
                {
                    { EPlantState.Idle , new AloecureIdleState(this)},
                    { EPlantState.Attack , new AloecureAttackState(this)},
                    { EPlantState.Select , new PlantSelectState(this)},
                    { EPlantState.Die, new PlantDieState(this)}
                },
                _ => new Dictionary<EPlantState, PlantState>
                {
                    { EPlantState.Idle , new PlantIdleState(this)},
                    { EPlantState.Attack , new PlantAttackState(this)},
                    { EPlantState.Select , new PlantSelectState(this)},
                    { EPlantState.Die, new PlantDieState(this)}
                }
            };
            
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

        public void ChangeState(EPlantState state)
        {
            if (_state == _states[state]) 
                return;
            
            _state.OnExit();
            _state = _states[state];
            _state.OnEnter();
        }

        public void Damage(float damage)
        {
            _currentHealth -= damage;
            
            StartCoroutine(Flash(Color.red));
        }
        
        public void Heal(float heal)
        {
            _currentHealth += heal;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _data.health);
            
            StartCoroutine(Flash(Color.green));
        }
        
        private IEnumerator Flash(Color color)
        {
            _spriteRenderer.color = color;
            yield return new WaitForSeconds(0.1f);
            _spriteRenderer.color = Color.white;
        }
    }
}