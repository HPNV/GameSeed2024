namespace Experience.States
{
    public class IdleState : IState
    {
        public void OnUpdate(ExperienceOrbBehaviour orb)
        {
            var direction = orb.Target - orb.transform.position;
            
            var distance = direction.magnitude;
            
            if (distance < orb.minimumDistance)
            {
                orb.ChangeState(State.Follow);
            }   
        }

        public void OnFixedUpdate(ExperienceOrbBehaviour orb)
        {
          
        }
    }
}