using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SIngletonGame : MonoBehaviour
{
    public static SIngletonGame Instance { get; private set; }
    private List<Card> availableCard = new List<Card>();
    private Inventory inventory;
    [SerializeField] public HomeBase homeBase;
    [SerializeField] private GameObject CardDisplayPrefab;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Set the instance to this object
            DontDestroyOnLoad(gameObject); // Make the object persistent across scenes
        }
        else
        {   
            Destroy(gameObject); // Destroy the duplicate instance
        }
    }

    void Start() {
        InitCardList();
    }

    public void SpawnPlant() {
        float offset = 6.5f; // Offset for spacing between plants
        Vector3 cameraPosition = Camera.main.transform.position;
        
        // Adjust the starting point to be slightly left of the screen's right edge
        float screenRightEdge = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, cameraPosition.z)).x;

        for (int i = 0; i < 3; i++) {
            // Calculate the position based on the adjusted right edge
            Vector3 spawnPosition = new Vector3(screenRightEdge + offset * i, cameraPosition.y, -5);

            // Instantiate the plant at the calculated world position
            GameObject x = Instantiate(CardDisplayPrefab, spawnPosition, Quaternion.identity, transform);

            // Get the CardDisplay component and assign a random card
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
}
