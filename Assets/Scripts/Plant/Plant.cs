using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Plant.States.Cactharn;
using Plant.States.Cobcorn;
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
            _state.Update();
        }

        public void Init()
        {
            GetComponent<Animator>().runtimeAnimatorController = Data.animatorController;
            _currentHealth = _data.health;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            
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
                },
                EPlant.Cobcorn => new Dictionary<EPlantState, PlantState>
                {
                    { EPlantState.Idle , new PlantIdleState(this)},
                    { EPlantState.Attack , new CobcornAttackState(this)},
                    { EPlantState.Select , new PlantSelectState(this)},
                },
                EPlant.Weisshooter => new Dictionary<EPlantState, PlantState>
                {
                    { EPlantState.Idle , new PlantIdleState(this)},
                    { EPlantState.Attack , new WeisshooterAttackState(this)},
                    { EPlantState.Select , new PlantSelectState(this)},
                },
                _ => new Dictionary<EPlantState, PlantState>
                {
                    { EPlantState.Idle , new PlantIdleState(this)},
                    { EPlantState.Attack , new PlantAttackState(this)},
                    { EPlantState.Select , new PlantSelectState(this)},
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
            
            StartCoroutine(FlashRed());
            
            if (_currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
        
        private IEnumerator FlashRed()
        {
            _spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            _spriteRenderer.color = Color.white;
        }
    }
}