using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using Card;
using Enemy;
using Manager;
using UnityEngine;
using Random = UnityEngine.Random;
using ResourceManager = Manager.ResourceManager;

public class SingletonGame : MonoBehaviour
{
    public static SingletonGame Instance { get; private set; }
    private List<CardData> availableCard = new List<CardData>();
    [SerializeField] public Inventory inventory;
    [SerializeField] public HomeBase homeBase;
    public int ExpPoint;
    private List<CardDisplay> cardDisplays = new List<CardDisplay>();


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
    foreach (var cardDisplay in cardDisplays) {
        cardDisplay.setCard(GetRandomCards(availableCard));
        cardDisplay.gameObject.SetActive(true);
    }
}

    private CardData GetRandomCards(List<CardData> cards)
    {
        return cards.OrderBy(c => Random.value).FirstOrDefault();
    }

    private void InitCardList() {
        for (int i=0;i<3;i++){
            CardData dummyCard = ScriptableObject.CreateInstance<CardData>();
            dummyCard.cardName = "Dummy Card" + i;
            dummyCard.description = "This is a dummy card description.";
            dummyCard.cardImage = Resources.Load<Sprite>("dummy");
            availableCard.Add(dummyCard);
        }
    }

    public void PickCard(CardData card)
    {
        inventory.AddCard(card);
        DestroyRemainingCards();
    }

    private void DestroyRemainingCards()
    {
        foreach (var cardDisplay in cardDisplays) {
            cardDisplay.gameObject.SetActive(false);
        }
    }

}
