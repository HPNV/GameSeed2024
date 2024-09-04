namespace Experience.States
{
    public abstract class BaseState : IState
    {
        protected ExperienceOrbBehaviour Orb { get; set; }

        protected BaseState(ExperienceOrbBehaviour orb)
        {
            Orb = orb;
        }
        
        public virtual void OnUpdate()
        {
            
        }

        public virtual void OnFixedUpdate()
        {
            
        }
    }
}