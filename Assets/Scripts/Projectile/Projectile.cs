﻿using System.Collections;
using Enemy;
using Projectile.Behaviour;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Projectile
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] 
        public ProjectileData data;
        public Vector2 Direction { get; set; } = Vector2.zero;
        public Vector2 Target { get; set; } = Vector2.zero;
        public IProjectileBehaviour Behaviour { get; set; }
        public GameObject SpriteObject { get; set; }
        
        
        private Coroutine _destroyCoroutine;
        private ParticleSystem _particles;
        
        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            var spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.sprite = data.sprite;
            transform.localScale = Vector3.one * data.scale;

            transform.rotation = Quaternion.Euler(0, 0, 0);
            
            if (!Direction.Equals(Vector2.zero))
            {
                var angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
            
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
            
            _destroyCoroutine = StartCoroutine(DestroyAfterTime());
            
            SpriteObject = transform.GetChild(0).gameObject;
            
            InitializeParticles();
            InitializeBehaviour();

            var circleCollider2D = GetComponent<CircleCollider2D>();
            
            circleCollider2D.excludeLayers = ~LayerMask.GetMask(data.targetLayer);
        }

        protected void Update()
        {
            Behaviour.Move();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(data.targetTag))
            {
                Behaviour.OnCollide(other);
            }
        }
        
        private IEnumerator DestroyAfterTime()
        {
            yield return new WaitForSeconds(data.lifetime);
            SingletonGame.Instance.ProjectileManager.Despawn(this);
        }
        
        private void OnDestroy()
        {
            if(_destroyCoroutine is not null)
                StopCoroutine(_destroyCoroutine);
        }

        private void InitializeParticles()
        {
            if (_particles != null)
            {
                _particles.Stop();
                Destroy(_particles.gameObject);
            }
            
            if (data.particles == null)
                return;
            
            _particles = Instantiate(data.particles, transform.position, Quaternion.identity);
            _particles.transform.parent = transform;
            _particles.Play();
        }
        
        private void InitializeBehaviour()
        {
            Behaviour = data.projectileType switch
            {
                ProjectileType.EnemyRanged => new EnemyProjectileBehaviour(this),
                ProjectileType.Cactharn => new CactharnProjectileBehaviour(this),
                ProjectileType.Cobcorn => new CobcornProjectileBehaviour(this),
                _ => null
            };
        }
    }
}