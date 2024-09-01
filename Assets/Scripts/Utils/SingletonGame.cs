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

    public ResourceManager ResourceManager { get; set; } = new();
    public ExperienceManager ExperienceManager { get; set; } = new();
    public ProjectileManager ProjectileManager { get; set; } = new();
    // public EnemyManager EnemyManager { get; set; } = new();


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
        // EnemyManager.Initialize();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            var x = Random.Range(-20, 20);
            var y = Random.Range(-20, 20);
            
            var position = transform.position;
            // EnemyManager.Spawn(EnemyType.Melee, new Vector2(position.x + x, position.y + y));
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            var x = Random.Range(-20, 20);
            var y = Random.Range(-20, 20);
            
            var position = transform.position;
                
            // EnemyManager.Spawn(EnemyType.Explosive, new Vector2(position.x + x, position.y + y));
        }
            
        if (Input.GetKeyDown(KeyCode.R))
        {
            var x = Random.Range(-20, 20);
            var y = Random.Range(-20, 20);
            
            var position = transform.position;
                
            // EnemyManager.Spawn(EnemyType.Ranged, new Vector2(position.x + x, position.y + y));
        }
    }

    void Start() {
        InitCardList();
    }

    public void SpawnPlant() {
        float offset = 6.5f;
        Vector3 cameraPosition = Camera.main.transform.position;
        float screenRightEdge = Camera.main.ViewportToWorldPoint(new Vector3(0.2f, 0.5f, cameraPosition.z)).x;

        for (int i = 0; i < 3; i++) {
            Vector3 spawnPosition = new Vector3(screenRightEdge + offset * i, cameraPosition.y, -5);
            GameObject x = Instantiate(CardDisplayPrefab, spawnPosition, Quaternion.identity, transform);
            CardDisplay cardDisplay = x.GetComponent<CardDisplay>();
            CardData card = GetRandomCards(availableCard);
            cardDisplay.cardData = card;

            Debug.Log(card);
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
        foreach (var cardDisplay in FindObjectsOfType<CardDisplay>()) {
            if (cardDisplay.cardData != null) {
                Destroy(cardDisplay.gameObject);
            }
        }
    }

}
