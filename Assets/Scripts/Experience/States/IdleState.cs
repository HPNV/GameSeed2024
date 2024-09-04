using UnityEngine;

namespace Experience.States
{
    public class IdleState : BaseState
    {
        public IdleState(ExperienceOrbBehaviour orb) : base(orb) {}
        
        public override void OnUpdate()
        {
            var direction = (Vector2)(Orb.Target - Orb.transform.position);
            
            var distance = direction.magnitude;

            if (distance < Orb.minimumDistance)
            {
                Orb.ChangeState(State.Follow);
            }
        }
    }
}