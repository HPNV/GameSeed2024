using UnityEngine;

namespace Plant.States
{
    public class PlantDieState : PlantState
    {
        private static readonly int DieTrigger = Animator.StringToHash("Die");

        public PlantDieState(Plant plant) : base(plant){}

        public override void Update()
        {
            var stateInfo = Plant.Animator.GetCurrentAnimatorStateInfo(0);
            
            if (stateInfo.IsName("Die") && stateInfo.normalizedTime >= 1)
            {
                Object.Destroy(Plant.gameObject);
            }
        }

        public override void OnEnter()
        {
            Object.Destroy(Plant.gameObject);
            
            SingletonGame.Instance.PlayerManager.OnPlantDie();
        }

        public override void OnExit()
        {
        }
        
    }
}