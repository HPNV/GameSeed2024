using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HomeBase : MonoBehaviour
{
    [SerializeField] private GameObject CardDisplayPrefab;
    [SerializeField] ExpBar expBar;
    private List<Card> availableCard;
    private int currentLevel = 1;
    private int currentExp = 0;
    private int expToNextLevel = 100;


    void Start()
    {
        availableCard = new List<Card>();
        SetExpBar();
        InitCardList();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GainExp(20);
        }
        
        SetExpBar();
        LevelUp();
    }

    private void LevelUp() {
        if(currentExp > expToNextLevel){
            currentLevel +=1;
            currentExp -= expToNextLevel;
            expToNextLevel += 50;
            SpawnPlant();
        }
    }

    private void GainExp(int exp) {
        currentExp += exp;
    }


    private void SetExpBar() {
        expBar.Exp = currentExp;
        expBar.setMaxExp(expToNextLevel);
    }

    private void SpawnPlant() {
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
