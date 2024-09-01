using System;
using Building;
using Enemy;
using Manager;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utils
{
    public class SingletonGame : MonoBehaviour
    {
        public static SingletonGame Instance { get; private set; }
        [SerializeField] 
        public HomeBase homeBase;
        
        public int ExpPoint;
    
        public ResourceManager ResourceManager { get; set; } = new();
        public ExperienceManager ExperienceManager { get; set; } = new();
        public ProjectileManager ProjectileManager { get; set; } = new();
        public EnemyManager EnemyManager { get; set; } = new();


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this; // Set the instance to this object
                Initialize();
                DontDestroyOnLoad(gameObject); // Make the object persistent across scenes
            }
            else
            {
                Destroy(gameObject); // Destroy the duplicate instance
            }
        }
    
        private void Initialize()
        {
            ResourceManager.Initialize();
            ExperienceManager.Initialize();
            ProjectileManager.Initialize();
            EnemyManager.Initialize();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                var x = Random.Range(-20, 20);
                var y = Random.Range(-20, 20);
            
                var position = transform.position;
                EnemyManager.Spawn(EnemyType.Melee, new Vector2(position.x + x, position.y + y));
            }
        
            if (Input.GetKeyDown(KeyCode.E))
            {
                var x = Random.Range(-20, 20);
                var y = Random.Range(-20, 20);
            
                var position = transform.position;
                
                EnemyManager.Spawn(EnemyType.Explosive, new Vector2(position.x + x, position.y + y));
            }
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                var x = Random.Range(-20, 20);
                var y = Random.Range(-20, 20);
            
                var position = transform.position;
                
                EnemyManager.Spawn(EnemyType.Ranged, new Vector2(position.x + x, position.y + y));
            }
        }
    }
}
