namespace Experience.States
{
    public interface IState
    {
        public void OnUpdate(ExperienceOrbBehaviour orb);
        public void OnFixedUpdate(ExperienceOrbBehaviour orb);
        public void OnEnter(ExperienceOrbBehaviour orb);
        public void OnExit(ExperienceOrbBehaviour orb);
    }
    
    public enum State
    {
        Idle,
        Follow,
        Collected
    }
}