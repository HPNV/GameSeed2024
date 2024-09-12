using System.Collections;
using Experience;
using UnityEngine;

namespace Plant.States.Swiftglory
{
    public class SwiftgloryIdleState : PlantIdleState
    {
        private Coroutine _speedCoroutine;
        public SwiftgloryIdleState(Plant plant) : base(plant) {}


        public override void OnEnter()
        {
            base.OnEnter();
            _speedCoroutine = Plant.StartCoroutine(Speed());
        }

        public override void Update() {}
        
        public override void OnExit()
        {
            base.OnExit();
            if (_speedCoroutine != null)
                Plant.StopCoroutine(_speedCoroutine);
        }
        
        private IEnumerator Speed()
        {
            yield return new WaitForSeconds(Plant.Data.attackCooldown);
            Plant.ChangeState(EPlantState.Attack);
        }
    }
}