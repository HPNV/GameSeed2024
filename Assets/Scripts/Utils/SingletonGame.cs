using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using Card;
using Enemy;
using Manager;
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
    [SerializeField] public CardDisplay card1;
    [SerializeField] public CardDisplay card2;
    [SerializeField] public CardDisplay card3;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject Tutorial1;
    [SerializeField] private GameObject Tutorial2;

    private int Tutorial1Check = 0;
    private int Tutorial2Check = 0;

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


    [SerializeField] private GameObject CardDisplayPrefab;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Initialize();
            DontDestroyOnLoad(gameObject);
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
        SoundFXManager.Initialize();
        SoundFXManager.instance.PlayMusic("Audio/Game Music"); 
        cardDisplays.Add(card1);
        cardDisplays.Add(card2);
        cardDisplays.Add(card3);
        
        _gameState = GameState.Play;

        if(false && PlayerManager.tutorialCompleted == 0) {
            Tutorial();
        }
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
                ResumeGame();
            }
        }

        if(homeBase.sun == 5 && Tutorial2Check == 0) {
            Tutorial2.SetActive(true);
            Tutorial2Check = 1;
            PauseGame();
        }

        if(Tutorial2Check == 1) {
            if(Input.GetKeyDown(KeyCode.Mouse0)) {
                Tutorial2Check = 2;
                Tutorial2.SetActive(false);
                ResumeGame();
            }
        }
    }

    private void Update()
    {
        if(PlayerManager.tutorialCompleted == 0) {
            checkTutorial();
        }
    }

    void Start() {
    }

    public void SpawnPlant() {
        PauseGame();
        SoundFXManager.instance.PlayGameSoundOnce("Audio/Level Up");
        HashSet<EPlant> assignedPlants = new HashSet<EPlant>();

        foreach (var cardDisplay in cardDisplays) {
            EPlant ePlant;
            
            //do {
                ePlant = plantFactory.GetRandomEPlant();
           // } while (assignedPlants.Contains(ePlant));

            assignedPlants.Add(ePlant);
            PlantData data = plantFactory.GetPlantData(ePlant);
            cardDisplay.SetCard(data, ePlant);
            cardDisplay.gameObject.SetActive(true);
        }
    }

    public void PickCard(EPlant plantType)
    {
        plantFactory.spawnPlant(plantType);
        ResumeGame();
        DestroyRemainingCards();
    }

    private void DestroyRemainingCards()
    {
        foreach (var cardDisplay in cardDisplays) {
            cardDisplay.gameObject.SetActive(false);
        }
    }
    

    public void PauseGame() {
        Time.timeScale = 0;
        IsPaused = true;
    }

    public void ResumeGame() {
        Time.timeScale = 1;
        IsPaused = false;
    }

    public void LoseGame()
    {
        PauseGame();
        loseScreen.SetActive(true);
        PlayerManager.OnPlayerDied();
    }
}
