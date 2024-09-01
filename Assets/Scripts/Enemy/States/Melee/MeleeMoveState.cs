using UnityEngine;

namespace Enemy.States.Melee
{
    public class MeleeMoveState : MoveState
    {
        public MeleeMoveState(EnemyBehaviour enemy) : base(enemy){} 

        public override void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Base"))
            {
                Enemy.ChangeState(State.Attack);
                Debug.Log("Change to attack");
            }   
        }
    }
}