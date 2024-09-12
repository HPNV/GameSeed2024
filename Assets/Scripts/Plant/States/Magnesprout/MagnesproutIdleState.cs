﻿using System.Collections;
using Experience;
using UnityEngine;

namespace Plant.States.Magnesprout
{
    public class MagnesproutIdleState : PlantIdleState
    {
        private Coroutine _delayCoroutine;
        public MagnesproutIdleState(Plant plant) : base(plant) {}

        public override void OnEnter()
        {
            _delayCoroutine = Plant.StartCoroutine(Delay());
        }

        public override void Update() {}

        public override void OnExit()
        {
            if (_delayCoroutine != null)
            {
                Plant.StopCoroutine(_delayCoroutine);
            }
        }

        private IEnumerator Delay()
        {
            yield return new WaitForSeconds(Plant.Data.attackCooldown);
            Plant.ChangeState(EPlantState.Attack);
        }
    }
}