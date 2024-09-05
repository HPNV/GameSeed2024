using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using Card;
using Enemy;
using Manager;
using Plant;
using UnityEngine;
using Random = UnityEngine.Random;
using ResourceManager = Manager.ResourceManager;

public class SingletonGame : MonoBehaviour
{
    public static SingletonGame Instance { get; private set; }
    [SerializeField] public HomeBase homeBase;
    [SerializeField] public PlantFactory plantFactory;

    [SerializeField] public CardDisplay card1;
    [SerializeField] public CardDisplay card2;
    [SerializeField] public CardDisplay card3;

    private List<CardDisplay> cardDisplays = new List<CardDisplay>();
    public int ExpPoint;
    private AudioClip gameMusic;
    [SerializeField] public CursorTileProviderService TileProvider;

    public ResourceManager ResourceManager { get; set; } = new();
    public ExperienceManager ExperienceManager { get; set; } = new();
    public ProjectileManager ProjectileManager { get; set; } = new();
    public EnemyManager EnemyManager { get; set; } = new();


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
        SoundFXManager.instance.PlayGameSound(Resources.Load<AudioClip>("Audio/Game Music")); 
        cardDisplays.Add(card1);
        cardDisplays.Add(card2);
        cardDisplays.Add(card3);
    }

    private void Update()
    {
        
    }

    void Start() {

    }

    public void SpawnPlant() {
        SoundFXManager.instance.PlayGameSoundOnce(Resources.Load<AudioClip>("Audio/Level Up"));
        foreach (var cardDisplay in cardDisplays) {
            EPlant ePlant = plantFactory.GetRandomEPlant();
            PlantData data = plantFactory.GetPlantData(ePlant);
            cardDisplay.SetCard(data,ePlant);
            cardDisplay.gameObject.SetActive(true);
        }
    }

    public void PickCard(EPlant plantType)
    {
        plantFactory.spawnPlant(plantType);
        DestroyRemainingCards();
    }

    private void DestroyRemainingCards()
    {
        foreach (var cardDisplay in cardDisplays) {
            cardDisplay.gameObject.SetActive(false);
        }
    }

}
