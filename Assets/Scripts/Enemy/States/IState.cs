namespace Enemy.States
{
    public interface IState
    {
        public void OnUpdate(EnemyBehaviour enemy);
        public void OnEnter(EnemyBehaviour enemy);
        public void OnExit(EnemyBehaviour enemy);
    }

    public enum State
    {
        Move,
        Attack,
        Die
    }
}