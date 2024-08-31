using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Experience.States
{
    public class FollowState : IState
    {
        public void OnUpdate(ExperienceOrbBehaviour orb)
        {
            var direction = (Vector2)(orb.Target - orb.transform.position);
            
            var distance = direction.magnitude;
            
            if (distance > orb.minimumDistance)
            {
                orb.ChangeState(State.Idle);
            }
            
            if (distance < orb.collectDistance)
            {
                orb.Destroy();
            }
        }

        public void OnFixedUpdate(ExperienceOrbBehaviour orb)
        {
            var direction = orb.Target - orb.transform.position;
            
            var distance = direction.magnitude;
            
            var forceMagnitude = Mathf.Clamp(100 * orb.maxForce / Mathf.Pow(distance, 3), 0, orb.maxForce);
            var force = direction.normalized * forceMagnitude;
            orb.Rigidbody.AddForce(force);
        }
    }
}