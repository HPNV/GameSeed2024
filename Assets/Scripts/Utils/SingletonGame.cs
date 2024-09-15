using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using Card;
using Enemy;
using Manager;
using Particles;
using Plant;
using Plant.Factory;
using UnityEngine;
using Random = UnityEngine.Random;
using ResourceManager = Manager.ResourceManager;
using Script;

public class SingletonGame : MonoBehaviour
{
    public static SingletonGame Instance { get; private set; }
    [SerializeField] public HomeBase homeBase;
    [SerializeField] public PlantFactory plantFactory;
    [SerializeField] public GameObject PickCardObject;
    [SerializeField] public CardDisplay card1;
    [SerializeField] public CardDisplay card2;
    [SerializeField] public CardDisplay card3;
    [SerializeField] public LoseScreen loseScreen;
    [SerializeField] private GameObject Tutorial1;
    [SerializeField] private GameObject Tutorial2;
    [SerializeField] private GameObject Tutorial3;
    [SerializeField] public AchivementHolder AchivementPrefab;

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
    public bool IsPaused { get; private set; }

    public int ExpPoint;

    public ResourceManager ResourceManager { get; set; } = new();
    public ExperienceManager ExperienceManager { get; set; } = new();
    public ProjectileManager ProjectileManager { get; set; } = new();
    public EnemyManager EnemyManager { get; set; } = new();
    public PlayerManager PlayerManager { get; set; } = new();
    public AchievementManager AchievementManager { get; set; } = new();
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
        ResourceManager.Initialize();
        ExperienceManager.Initialize();
        ProjectileManager.Initialize();
        EnemyManager.Initialize();
        ParticleManager.Initialize();
        CursorManager.Initialize();
        SoundFXManager.Initialize();
        SoundFXManager.instance.PlayMusic("Audio/Game Music"); 
        PickCardObject.SetActive(false);
        cardDisplays.Add(card1);
        cardDisplays.Add(card2);
        cardDisplays.Add(card3);
        
        _gameState = GameState.Play;

        if(true && PlayerManager.tutorialCompleted == 0) {
            Tutorial();
        } else {
            // SpawnPlant();
            PickCardObject.SetActive(true);
            Tutorial1.SetActive(false);
            Tutorial1Check = 5;
            Tutorial2Check = 5;
            Tutorial3Check = 5;
        }
        
        CursorManager.ChangeCursor(CursorType.Arrow);
    }

    private void Tutorial() {
        PauseGame();
        Tutorial1.SetActive(true);
    }

    private void checkTutorial() {
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
                PlayerManager.tutorialCompleted = 1;
                ResumeGame();
            }
        }
        
    }

    private void Update()
    {
        if(true && PlayerManager.tutorialCompleted == 0) {
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
    
    public void PauseGame() {
        Time.timeScale = 0;
        IsPaused = true;
    }

    public void ResumeGame() {
        Time.timeScale = 1;
        IsPaused = false;
    }

    public void addEnemyKilled() {
        enemyKilled += 1;
    }

    public void addPlantPlanted() {
        plantPlanted += 1;
    }

    public void LoseGame()
    {
        PauseGame();
        loseScreen.gameObject.SetActive(true);
        loseScreen.UpdateUI(homeBase.score, enemyKilled, plantPlanted);
        PlayerManager.OnPlayerDied();
    }
}
