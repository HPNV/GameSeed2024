using System;
using System.Collections.Generic;
using Building;
using Experience.States;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Experience
{
    public class ExperienceOrbBehaviour : MonoBehaviour
    {
        [SerializeField] 
        public float maxForce = 50f;
        [SerializeField] 
        public float minimumDistance = 1000;
        [SerializeField] 
        public float collectDistance = 1;
        [SerializeField] 
        public float scale = 1;
         
        public int ExperienceCount { get; set; }

        public int experienceValue;

        public Vector3 Target { get; set; }
        public Rigidbody2D Rigidbody  { get; set; }

        private IState _currentState;
        private Dictionary<State, IState> _states;
        private Camera _camera;
        
        private void Start()
        {
            experienceValue = Random.Range(5, 11);

            Rigidbody = GetComponent<Rigidbody2D>();
            
            var randomForce = new Vector2(Random.Range(-maxForce, maxForce), Random.Range(-maxForce, maxForce));
            Rigidbody.AddForce(randomForce * 10);
            Rigidbody.drag = 1;
            
            transform.localScale = new Vector3(scale, scale, 1);
            
            _camera = Camera.main;
            _states = new Dictionary<State, IState>
            {
                {State.Idle, new IdleState(this)},
                {State.Follow, new FollowState(this)},
            };
            _currentState = _states[State.Idle];
        }
    
        private void Update()
        {
            Target = _camera.ScreenToWorldPoint(Input.mousePosition);
            _currentState.OnUpdate();
        }
        
        private void FixedUpdate()
        { 
            _currentState.OnFixedUpdate();
        }
        
        public void ChangeState(State state)
        {
            _currentState = _states[state];
        }

        public void Reset()
        {
            Start();
        }
    }   
}
