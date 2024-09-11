using System.Collections;
using Experience;
using UnityEngine;

namespace Plant.States.Aloecura
{
    public class AloecuraIdleState : PlantIdleState
    {
        private Coroutine _healCoroutine;
        public AloecuraIdleState(Plant plant) : base(plant) {}


        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("ALOECURE IDLE STATE");
            _healCoroutine = Plant.StartCoroutine(Heal());
        }

        public override void Update() {}
        
        public override void OnExit()
        {
            base.OnExit();
            if (_healCoroutine != null)
                Plant.StopCoroutine(_healCoroutine);
        }
        
        private IEnumerator Heal()
        {
            yield return new WaitForSeconds(Plant.Data.attackCooldown);
            Plant.ChangeState(EPlantState.Attack);
        }
    }
}