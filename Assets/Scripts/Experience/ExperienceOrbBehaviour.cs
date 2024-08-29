using System;
using System.Collections;
using System.Collections.Generic;
using Experience.States;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Experience
{
    public class ExperienceOrbBehaviour : MonoBehaviour
    {
        [SerializeField] 
        public float maxForce = 500f;
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
            Rigidbody = GetComponent<Rigidbody2D>();
            
            var randomForce = new Vector2(Random.Range(-maxForce, maxForce), Random.Range(-maxForce, maxForce));
            Rigidbody.AddForce(randomForce);
            Rigidbody.drag = 1;
            
            _camera = Camera.main;
            _states = new Dictionary<State, IState>
            {
                {State.Idle, new IdleState()},
                {State.Follow, new FollowState()},
            };
            _currentState = _states[State.Idle];
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
        
        public void ChangeState(State state)
        {
            _currentState = _states[state];
        }
        
        public void Destroy()
        {
            Destroy(gameObject);
        }
    }   
}
