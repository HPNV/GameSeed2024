using UnityEngine;

namespace Enemy.States
{
    public interface IState
    {
        public void OnUpdate();
        public void OnEnter();
        public void OnExit();
        public void OnCollisionStay2D(Collision2D collision);
    }

    public enum State
    {
        Move,
        Attack,
        Die,
        Explode
    }
}