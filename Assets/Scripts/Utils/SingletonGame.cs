using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils
{
    public class SingletonGame : MonoBehaviour
    {
        public static SingletonGame Instance { get; private set; }
        private List<Card> availableCard = new List<Card>();
    [SerializeField] public Inventory inventory;
    [SerializeField] public HomeBase homeBase;
        public int ExpPoint;
    
        public ResourceManager ResourceManager { get; set; } = new();
        public ExperienceManager ExperienceManager { get; set; } = new();
        public ProjectileManager ProjectileManager { get; set; } = new();


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
            Card card = GetRandomCards(availableCard);
            cardDisplay.card = card;

            Debug.Log(card);
        }
    }

    private Card GetRandomCards(List<Card> cards)
    {
        return cards.OrderBy(c => Random.value).FirstOrDefault();
    }

    private void InitCardList() {
        for (int i=0;i<3;i++){
            Card dummyCard = ScriptableObject.CreateInstance<Card>();
            dummyCard.cardName = "Dummy Card" + i;
            dummyCard.description = "This is a dummy card description.";
            dummyCard.cardImage = Resources.Load<Sprite>("dummy");
            availableCard.Add(dummyCard);
        }
    }

    public void PickCard(Card card)
    {
        inventory.AddCard(card);
        DestroyRemainingCards();
    }

    private void DestroyRemainingCards()
    {
        foreach (var cardDisplay in FindObjectsOfType<CardDisplay>()) {
            if (cardDisplay.card != null) {
                Destroy(cardDisplay.gameObject);
            }
        }
    }
}
