using System;
using System.Collections;
using System.Collections.Generic;
using Experience.States;
using UnityEngine;

namespace Experience
{
    public class ExperienceOrbBehaviour : MonoBehaviour
    {
        [SerializeField] 
        public float maxForce = 100f;
        [SerializeField] 
        public float minimumDistance = 1000;
        [SerializeField] 
        public float collectDistance = 1;
        
        public int ExperienceCount { get; set; }

        public Vector3 Target { get; set; }
        public Rigidbody2D Rigidbody  { get; set; }

        private IState _currentState;
        private Dictionary<State, IState> _states;
        private Camera _camera;
        
        
        
        private void Start()
        {
            maxForce = 100f;
            collectDistance = 10;
            Rigidbody = GetComponent<Rigidbody2D>();
            _camera = Camera.main;
            _states = new Dictionary<State, IState>
            {
                {State.Idle, new IdleState()},
                {State.Follow, new FollowState()},
                {State.Collected, new CollectedState()}
            };
            _currentState = _states[State.Idle];
            _currentState.OnEnter(this);
            
        }
    
        private void Update()
        {
            Target = _camera.ScreenToWorldPoint(Input.mousePosition);
            _currentState.OnUpdate(this);
        }
        
        private void FixedUpdate()
        { 
            _currentState.OnFixedUpdate(this);
        }

        private void OnDestroy()
        {
            _currentState.OnExit(this);
        }
        
        public void ChangeState(State state)
        {
            _currentState.OnExit(this);
            _currentState = _states[state];
            _currentState.OnEnter(this);
        }
        
        public void Destroy()
        {
            Destroy(gameObject);
        }
    }   
}
