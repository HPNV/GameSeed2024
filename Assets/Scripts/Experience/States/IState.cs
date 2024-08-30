namespace Experience.States
{
    public interface IState
    {
        public void OnUpdate(ExperienceOrbBehaviour orb);
        public void OnFixedUpdate(ExperienceOrbBehaviour orb);
    }
    
    public enum State
    {
        Idle,
        Follow,
    }
}