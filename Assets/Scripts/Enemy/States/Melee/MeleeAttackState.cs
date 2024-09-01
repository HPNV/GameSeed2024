namespace Enemy.States.Melee
{
    public class MeleeAttackState : AttackState
    {
        public MeleeAttackState(EnemyBehaviour enemy) : base(enemy){} 
        
        
        public override void OnUpdate()
        {
            base.OnUpdate();
            var stateInfo = Enemy.Animator.GetCurrentAnimatorStateInfo(0);
            
            if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 1)
            {
                Enemy.ChangeState(State.Move);
            }
        }
    }
}