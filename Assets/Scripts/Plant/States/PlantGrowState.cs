﻿using UnityEngine;

namespace Plant.States
{
    public class PlantGrowState : PlantState
    {
        private static readonly int Grow = Animator.StringToHash("Grow");

        public PlantGrowState(Plant plant) : base(plant)
        {
        }

        public override void Update()
        {
            var stateInfo = Plant.Animator.GetCurrentAnimatorStateInfo(0);
            
            if (stateInfo.IsName("Grow") && stateInfo.normalizedTime >= 0.8)
            {
                Plant.ChangeState(EPlantState.Idle);
            }
        }

        public override void OnEnter()
        {
            SingletonGame.Instance.addPlantPlanted();
            // SingletonGame.Instance.AchievementManager
            Plant.Animator.SetTrigger(Grow);
            Plant.Animator.speed = 1 / Plant.Data.growTimeMultiplier;
        }

        public override void OnExit()
        {
           Plant.Animator.speed = 1;
        }
    }
}