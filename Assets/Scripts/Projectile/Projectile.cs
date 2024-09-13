using System;
using System.Collections;
using System.Text;
using Enemy;
using Plant;
using Projectile.Behaviour;
using Unity.VisualScripting;
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
        public float AttackPower { get; set; }
        public GameObject SpriteObject { get; set; }
        public SpriteRenderer SpriteRenderer { get; set; }
        public Animator Animator { get; set; }
        
        
        private Coroutine _destroyCoroutine;
        private ParticleSystem _particles;
        
        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
            Animator = GetComponentInChildren<Animator>();
            
            if (data.textureType == TextureType.Static)
            {
                Animator.runtimeAnimatorController = null;
                Animator.enabled = false;
                SpriteRenderer.sprite = data.sprite;
            }
            
            if (data.textureType == TextureType.Animated)
            {
                Animator.runtimeAnimatorController = data.animatorController;
                SpriteRenderer.sprite = null;
                Animator.enabled = true;
            }
            
            var color = SpriteRenderer.color;
            color.a = 1;
            SpriteRenderer.color = color;
            
            transform.localScale = Vector3.one * data.scale;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            
            if (!Direction.Equals(Vector2.zero))
            {
                var angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
            
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
            
            InitializeParticles();
            InitializeBehaviour();
            
            _destroyCoroutine = StartCoroutine(DestroyAfterTime());
            
            SpriteObject = transform.GetChild(0).gameObject;
            SpriteObject.transform.localScale = Vector3.one;
            

            var circleCollider2D = GetComponent<CircleCollider2D>();

            circleCollider2D.includeLayers = LayerMask.GetMask(data.targetLayer);
            circleCollider2D.excludeLayers = ~LayerMask.GetMask(data.targetLayer);
        }

        protected void Update()
        {
            Behaviour.Update();
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(data.targetTag))
            {
                var plant = other.gameObject.GetComponent<Plant.Plant>();

                if (plant != null && plant.Data.plantType == EPlant.Bamburst)
                    return;
                
                Behaviour.OnCollide(other);
            }
        }
        
        private IEnumerator DestroyAfterTime()
        {
            yield return new WaitForSeconds(data.lifetime);
            Behaviour.OnDespawn();
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
            
            if (!Direction.Equals(Vector2.zero))
            {
                var particlesMain = _particles.main;
                
                particlesMain.startRotation = -Mathf.Atan2(Direction.y, Direction.x);
            }
            
            _particles.Play();
        }
        
        private void InitializeBehaviour()
        {
            Behaviour = data.projectileType switch
            {
                ProjectileType.Enemy => new EnemyProjectileBehaviour(this),
                ProjectileType.Piercing => new PiercingProjectileBehaviour(this),
                ProjectileType.Mortar => new MortarProjectileBehaviour(this),
                ProjectileType.SingleHit => new SingleHitProjectileBehaviour(this),
                ProjectileType.Healing => new HealingProjectileBehaviour(this),
                ProjectileType.SpeedUp => new SpeedingProjectileBehaviour(this),
                ProjectileType.ExplosiveMortar => new ExplosiveMortarProjectileBehaviour(this),
                ProjectileType.Knockback => new KnockbackProjectileBehaviour(this),
                _ => null
            };
            
        }
    }
}