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
    private List<PlantData> availableCard = new List<PlantData>();
    [SerializeField] public HomeBase homeBase;
    [SerializeField] public PlantFactory plantFactory;
    public int ExpPoint;
    private List<CardDisplay> cardDisplays = new List<CardDisplay>();
    private AudioClip gameMusic;
    [SerializeField] public CursorTileProviderService TileProvider;
    // [SerializeField] public TileProviderService TileProvider;

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
    }

    private void Update()
    {
        
    }

    void Start() {
        InitCardList();
        initCard();
    }

    public void initCard() {
        float offset = 0.25f;
        Vector3 cameraPosition = Camera.main.transform.position;
        for (int i = 0; i < 3; i++) {
            float screenRightEdge = Camera.main.ViewportToWorldPoint(new Vector3(0.25f + (offset * i), 0.5f, cameraPosition.z)).x;
            Vector3 spawnPosition = new Vector3(screenRightEdge, cameraPosition.y, -5);
            GameObject x = Instantiate(CardDisplayPrefab, spawnPosition, Quaternion.identity, transform);
            CardDisplay cardDisplay = x.GetComponent<CardDisplay>();
            cardDisplays.Add(cardDisplay);
            x.SetActive(false);
        }
    }

    public void SpawnPlant() {
        SoundFXManager.instance.PlayGameSoundOnce(Resources.Load<AudioClip>("Audio/Level Up"));
        Vector3 cameraPosition = Camera.main.transform.position;
        float offset = 0.25f;
        for (int i = 0; i < cardDisplays.Count; i++) {
            float screenRightEdge = Camera.main.ViewportToWorldPoint(new Vector3(0.25f + (offset * i), 0.5f, cameraPosition.z)).x;
            Vector3 newPosition = new Vector3(screenRightEdge, cameraPosition.y, -5);
            EPlant ePlant = plantFactory.GetRandomEPlant();
            PlantData data = plantFactory.GetPlantData(ePlant);
            cardDisplays[i].transform.position = newPosition;
            cardDisplays[i].SetCard(data,ePlant);
            cardDisplays[i].gameObject.SetActive(true);
        }
    }

    private void InitCardList() {
        for (int i=0;i<3;i++){
            PlantData dummyCard = ScriptableObject.CreateInstance<PlantData>();
            dummyCard.plantName = "dummy";
            dummyCard.health = 100;
            dummyCard.damage = 10;
            dummyCard.cd = 0;
            dummyCard.range = 6;
            dummyCard.animatorController = null;
            availableCard.Add(dummyCard);
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
