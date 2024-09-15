using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using Card;
using Enemy;
using Firebase.Extensions;
using Firebase.Firestore;
using JetBrains.Annotations;
using Manager;
using Particles;
using Plant;
using Plant.Factory;
using UnityEngine;
using ResourceManager = Manager.ResourceManager;
using Script;
using UnityEngine.SceneManagement;

public class SingletonGame : MonoBehaviour
{
    public static SingletonGame Instance { get; private set; }
    [SerializeField] public HomeBase homeBase;
    [SerializeField] public PlantFactory plantFactory;
    [SerializeField] [CanBeNull] public GameObject PickCardObject;
    [SerializeField] public CardDisplay card1;
    [SerializeField] public CardDisplay card2;
    [SerializeField] public CardDisplay card3;
    [SerializeField] public LoseScreen loseScreen;
    [SerializeField] private GameObject Tutorial1;
    [SerializeField] private GameObject Tutorial2;
    [SerializeField] private GameObject Tutorial3;
    [SerializeField] public AchivementHolder AchivementPrefab;
    
    FirebaseFirestore db;

    private const int CARD_AMOUNT = 3;
    private int Tutorial1Check = 0;
    private int Tutorial2Check = 0;
    private int Tutorial3Check = 0;
    private int enemyKilled = 0;
    private int plantPlanted = 0;

    private List<CardDisplay> cardDisplays = new List<CardDisplay>();
    private AudioClip gameMusic; 
    private GameState _gameState;
    
    public TileService TileProvider;
    public GameGrid GameGrid;
<<<<<<< Updated upstream
    public bool IsPaused { get; set; }
=======
    public System.Random Random = new System.Random();
    public bool IsPaused { get; private set; }
>>>>>>> Stashed changes

    public int ExpPoint;

    public ResourceManager ResourceManager { get; set; } = new();
    public ExperienceManager ExperienceManager { get; set; } = new();
    public ProjectileManager ProjectileManager { get; set; } = new();
    public EnemyManager EnemyManager { get; set; } = new();
    public AchievementManager AchievementManager { get; set; }
    public ParticleManager ParticleManager { get; set; } = new();
    public CursorManager CursorManager { get; set; } = new();


    [SerializeField] private GameObject CardDisplayPrefab;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Initialize();
            // DontDestroyOnLoad(gameObject);
        }
        else
        {   
            Destroy(gameObject);
        }
    }

    private void Initialize()
    {
        db = FirebaseFirestore.DefaultInstance;
        ResourceManager.Initialize();
        ExperienceManager.Initialize();
        ProjectileManager.Initialize();
        EnemyManager.Initialize();
        ParticleManager.Initialize();
        CursorManager.Initialize();
        SoundFXManager.Initialize();
        SoundFXManager.instance.PlayMusic("Audio/Game Music"); 
        if (PickCardObject != null)
            PickCardObject.SetActive(false);
        cardDisplays.Add(card1);
        cardDisplays.Add(card2);
        cardDisplays.Add(card3);

        AchievementManager = AchievementManager.Instance;
        
        _gameState = GameState.Play;

        if(true && PlayerManager.Instance.tutorialCompleted == 0) {
            Tutorial();
        } else {
            // SpawnPlant();
            PickCardObject.SetActive(true);
            Tutorial1.SetActive(false);
            Tutorial1Check = 5;
            Tutorial2Check = 5;
            Tutorial3Check = 5;
        }
        
        fetchUserData();
        
        CursorManager.ChangeCursor(CursorType.Arrow);
    }
    
    private void fetchUserData()
    {
        string hostname = System.Environment.MachineName;
        DocumentReference docRef = db.Collection("users").Document(hostname);

        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot snapshot = task.Result;

            if (snapshot.Exists)
            {
                Dictionary<string, object> data = snapshot.ToDictionary();
                // Debug.Log($"RWARRRR {data["die_counter"]}");
                var player = PlayerManager.Instance;
                player.Die = Convert.ToInt32(data["die_counter"]);
                player.Kill = Convert.ToInt32(data["kill_counter"]);
                player.PlantedPlants = Convert.ToInt32(data["plant_counter"]);
                player.EnemyExplodeCounter = Convert.ToInt32(data["explode_counter"]);
                player.CollectResourceCounter = Convert.ToInt32(data["resource_counter"]);
                player.UpgradePlantCounter = Convert.ToInt32(data["upgrade_counter"]);
                player.LevelUpCounter = Convert.ToInt32(data["level_up_counter"]);
                Instance.homeBase.score = Convert.ToInt32(data["highest_score"]);
                player.FullUpgradePlantCounter = Convert.ToInt32(data["max_upgrade"]);
                player.CompleteTutorial = Convert.ToBoolean(data["complete_tutorial"]);
            }
            else
            {
                // Debug.Log("Document " + snapshot.Id + " does not exist!");
            }
        });
    }

    private void Tutorial()     
    {
        PauseGame();
        if (Tutorial1 != null)
            Tutorial1.SetActive(true);
    }

    private void checkTutorial()
    {
        // if (homeBase == null)
        //     return;
        
        if(Tutorial1Check == 0) {
            if(Input.GetKeyDown(KeyCode.Mouse0)) {
                Tutorial1Check = 1;
                Tutorial1.SetActive(false);
                PickCardObject.SetActive(true);
            }
        }

        if(homeBase.sun >= 5 && Tutorial2Check == 0) {
            Tutorial2.SetActive(true);
            Tutorial2Check = 1;
            PauseGame();
        }

        if(homeBase.water >= 5 && Tutorial3Check == 0) {
            Tutorial3.SetActive(true);
            Tutorial3Check = 1;
            PauseGame();
        }

        if(Tutorial2Check == 1) {
            if(Input.GetKeyDown(KeyCode.Mouse0)) {
                Tutorial2Check = 2;
                Tutorial2.SetActive(false);
                ResumeGame();
            }
        }

        if(Tutorial3Check == 1) {
            if(Input.GetKeyDown(KeyCode.Mouse0)) {
                Tutorial3Check = 2;
                Tutorial3.SetActive(false);
                PlayerManager.Instance.tutorialCompleted = 1;
                ResumeGame();
            }
        }
        
    }

    private void Update()
    {
        if(true && PlayerManager.Instance.tutorialCompleted == 0) {
            checkTutorial();
        }
    }

    void Start() {
    }

    public void SpawnPlant() {
        PauseGame();
        SoundFXManager.instance.PlayGameSoundOnce("Audio/Level Up");

        var assignedPlants = plantFactory.GetUnlockedPlants(CARD_AMOUNT);
        for (var i = 0; i < CARD_AMOUNT; i++)
        {
            cardDisplays[i].SetCard(assignedPlants[i]);
        }

        PickCardObject.SetActive(true);
    }

    public void PickCard(EPlant plantType)
    {
        plantFactory.spawnPlant(plantType);
        ResumeGame();
        DestroyRemainingCards();
    }
    
    public void PickCard(PlantData plantData)
    {
        plantFactory.SpawnPlant(plantData);
        ResumeGame();
        DestroyRemainingCards();
    }

    private void DestroyRemainingCards()
    {
        PickCardObject.SetActive(false);
    }
    
    public void PauseGame() 
    {
        Time.timeScale = 0;
        IsPaused = true;
    }

    public void ResumeGame() 
    {
        Time.timeScale = 1;
        IsPaused = false;
    }

    public void addEnemyKilled() {
        enemyKilled += 1;
    }

    public void addPlantPlanted() {
        plantPlanted += 1;
    }

    public void SaveData()
    {
        Dictionary<string, object> data = new Dictionary<string, object>
        {
            { "die_counter", PlayerManager.Instance.Die + 1 },
            { "kill_counter", PlayerManager.Instance.Kill },
            { "plant_counter", PlayerManager.Instance.Planted },
            { "full_upgrade_plant_counter", PlayerManager.Instance.FullUpgradePlantCounter },
            { "collect_resource_counter", PlayerManager.Instance.CollectResourceCounter },
            { "unlocked_achievements", PlayerManager.Instance.UnlockedAchievements },
            { "sacrifice_counter", PlayerManager.Instance.SacrificeCounter },
            { "planted_plants_counter", PlayerManager.Instance.PlantedPlants },
            { "level_up_counter", PlayerManager.Instance.LevelUpCounter },
            { "highest_score", PlayerManager.Instance.HighScore },
            { "complete_tutorial", PlayerManager.Instance.CompleteTutorial },
            { "survival_data", PlayerManager.Instance.SurvivalData },
            { "active_plant_data", PlayerManager.Instance.ActivePlantData },
            { "explosive_data", PlayerManager.Instance.ExplosiveData },
            { "planted_in_time_data", PlayerManager.Instance.PlantedInTimeData },
            { "utils_data", PlayerManager.Instance.UtilsData }
        };
        
        DocumentReference docRef = db.Collection("users").Document(System.Environment.MachineName);
        
        docRef.SetAsync(data).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                // Debug.Log("Document written with ID: " + docRef.Id);
            }
        });
    }
    
    
    public void LoseGame()
    {
        SaveData();
        
        PauseGame();
        loseScreen.gameObject.SetActive(true);
        loseScreen.UpdateUI(homeBase.score, enemyKilled, plantPlanted);
        PlayerManager.Instance.OnPlayerDied();

        StartCoroutine(WaitForInputAndLoadScene(0));
    }
    
    IEnumerator WaitForInputAndLoadScene(int sceneId)
    {
        Debug.Log("taet");
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;  // Wait for the next frame
        }

        // Now start loading the scene asynchronously
        StartCoroutine(LoadScene(sceneId));
    }
    
    IEnumerator LoadScene(int sceneId)
    {
        // Now load the scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneId);

        // Deactivate automatic scene activation
        asyncLoad.allowSceneActivation = false;
        
        while (asyncLoad.progress < 0.9f)
        {
            float loadProgress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            yield return null;
        }
        
        StopAllCoroutines();
        Time.timeScale = 1;
        asyncLoad.allowSceneActivation = true;
    }
}
