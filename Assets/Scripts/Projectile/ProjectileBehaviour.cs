using System.Collections;
using Enemy;
using UnityEngine;

namespace Projectile
{
    public class ProjectileBehaviour : MonoBehaviour
    {
        [SerializeField] 
        public ProjectileData data;
        public Vector2 Direction { get; set; }
        public string TargetTag { get; set; }
        
        private Coroutine _destroyCoroutine;
        private ParticleSystem _particles;
        
        private void Start()
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = data.sprite;
            transform.localScale = Vector3.one * data.scale;
            _destroyCoroutine = StartCoroutine(DestroyAfterTime());
            InitializeParticles();
        }

        private void Update()
        {
            var currentPosition = transform.position;
            
            transform.position = Vector2.MoveTowards(
                currentPosition,
                (Vector2)currentPosition + Direction,  
                data.movementSpeed * Time.deltaTime);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(TargetTag))
            {
                var enemy = other.gameObject.GetComponent<EnemyBehaviour>();
                //enemy.TakeDamage(data.attackPower);
                Destroy(gameObject);
            }
        }
        
        private IEnumerator DestroyAfterTime()
        {
            yield return new WaitForSeconds(data.lifetime);
            Destroy(gameObject);
        }
        
        private void OnDestroy()
        {
            if(_destroyCoroutine is not null)
                StopCoroutine(_destroyCoroutine);
        }

        private void InitializeParticles()
        {
            _particles = Instantiate(data.particles, transform.position, Quaternion.identity);
            _particles.transform.parent = transform;
            _particles.Play();
        }
    }
}