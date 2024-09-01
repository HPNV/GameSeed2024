using Building;
using Manager;
using UnityEngine;

namespace Utils
{
    public class SingletonGame : MonoBehaviour
    {
        public static SingletonGame Instance { get; private set; }
        [SerializeField] public HomeBase homeBase;
        public int ExpPoint;
    
        public ResourceManager ResourceManager { get; set; } = new();
        public ExperienceManager ExperienceManager { get; set; } = new();
        public ProjectileManager ProjectileManager { get; set; } = new();


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
        }

        void Start() {

        }
    }
}
