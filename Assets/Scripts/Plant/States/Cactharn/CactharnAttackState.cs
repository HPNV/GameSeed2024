namespace Plant.States.Cactharn
{
    public class CactharnAttackState : PlantAttackState
    {
        public CactharnAttackState(Plant plant) : base(plant){}

        public override void Update()
        {
            base.Update();
            
            var stateInfo = Plant.Animator.GetCurrentAnimatorStateInfo(0);
            
            if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 1)
            {
                
            }
        }
    }
}