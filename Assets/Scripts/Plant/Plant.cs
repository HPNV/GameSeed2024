using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

namespace Plant
{
    public class Plant : MonoBehaviour
    {
        [SerializeField]
        private PlantData data;

        private Dictionary<EPlantState, PlantState> _states; 
        private PlantState _state;
        public TargetService TargetService { get; private set; }
    
        private void Start()
        {
            GetComponent<Animator>().runtimeAnimatorController = data.animatorController;

            _states = new Dictionary<EPlantState, PlantState>
            {
                { EPlantState.Idle , new PlantIdleState(this)},
                { EPlantState.Attack , new PlantAttackState(this)}
            };
            _state = _states[EPlantState.Idle];
            InitTargetService();
        }

        private void Update()
        {
            _state.Update();
        }

        private void InitTargetService()
        {
            switch (data.targetType)
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
            _state = _states[state];
        }
    }
}