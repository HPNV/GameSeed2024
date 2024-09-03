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
            GetComponent<Animator>().runtimeAnimatorController = Data.animatorController;

            _states = new Dictionary<EPlantState, PlantState>
            {
                { EPlantState.Idle , new PlantIdleState(this)},
                { EPlantState.Attack , new PlantAttackState(this)}
            };
            _state = _states[EPlantState.Idle];
            InitTargetService();
        }

        private void FixedUpdate()
        {
            _state.Update();
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
            _state.OnExit();
            _state = _states[state];
            _state.OnEnter();
        }
    }
}