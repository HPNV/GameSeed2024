using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

namespace Plant
{
    public class Plant : MonoBehaviour
    {
        [field: SerializeField]
        public PlantData Data { get; set; }

        private Dictionary<EPlantState, PlantState> _states; 
        private PlantState _state;

        [field: SerializeField]
        public Animator Animator { get; private set; }
        public TargetService TargetService { get; private set; }
    
        private void Start()
        {
            Init();
        }

        private void FixedUpdate()
        {
            _state.Update();
        }

        public void Init()
        {
            GetComponent<Animator>().runtimeAnimatorController = Data.animatorController;

            InitState();
            InitTargetService();
        }

        private void InitState()
        {
            if (_state != null && _states != null) return;
            _states = new Dictionary<EPlantState, PlantState>
            {
                { EPlantState.Idle , new PlantIdleState(this)},
                { EPlantState.Attack , new PlantAttackState(this)},
                { EPlantState.Select , new PlantSelectState(this)},
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
                default:
                    break;
            }
        }

        public void ChangeState(EPlantState state)
        {
            Debug.Log($"state: {_state}");
            Debug.Log($"states: {_states}");
            if (_state == _states[state]) return;
            _state.OnExit();
            _state = _states[state];
            _state.OnEnter();
        }
    }
}