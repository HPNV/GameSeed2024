namespace Experience.States
{
    public interface IState
    {
        public void OnUpdate();
        public void OnFixedUpdate();
    }
    
    public enum State
    {
        Idle,
        Follow,
    }
}