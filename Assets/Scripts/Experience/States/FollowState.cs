﻿using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Experience.States
{
    public class FollowState : BaseState
    {
        public FollowState(ExperienceOrbBehaviour orb) : base(orb) {}
        
        public override void OnUpdate()
        {
            var direction = (Vector2)(Orb.Target - Orb.transform.position);
            
            var distance = direction.magnitude;
            
            if (distance > Orb.minimumDistance)
            {
                Orb.ChangeState(State.Idle);
            }
            
            if (SingletonGame.Instance.IsPaused)
                return;
            
            if (distance < Orb.collectDistance)
            {
                SingletonGame.Instance.homeBase.GainExp(Orb.experienceValue * 5);
                SingletonGame.Instance.ExperienceManager.Despawn(Orb);
            } 
        }

        public override void OnFixedUpdate()
        {
            var direction = (Vector2)(Orb.Target - Orb.transform.position);
            
            var distance = direction.magnitude;
            
            var forceMagnitude = Mathf.Clamp(100 * Orb.maxForce / Mathf.Pow(distance, 3), 0, Orb.maxForce);
            var force = direction.normalized * forceMagnitude;
            Orb.Rigidbody.AddForce(force);
        }
    }
}