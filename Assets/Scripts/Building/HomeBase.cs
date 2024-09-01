using System.Collections.Generic;
using System.Linq;
using Card;
using UnityEngine;
using Utils;

namespace Building
{
    public class HomeBase : MonoBehaviour
    {
        [SerializeField] private GameObject CardDisplayPrefab;
        [SerializeField] ExpBar expBar;
        private List<Card.Card> availableCard;
        private int currentLevel = 1;
        private int currentExp = 0;
        private int expToNextLevel = 100;


        void Start()
        {
            availableCard = new List<Card.Card>();
            SetExpBar();
            InitCardList();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                mousePosition.z = -2;
                SingletonGame.Instance.ExperienceManager.Spawn(1,mousePosition);
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

        public void GainExp(int exp) {
            currentExp += exp;
        }


        public void SetExpBar() {
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
                Card.Card card = GetRandomCards(availableCard);
                cardDisplay.card = card;

                Debug.Log(card);
            }
        }


        private Card.Card GetRandomCards(List<Card.Card> cards)
        {
            return cards.OrderBy(c => Random.value).FirstOrDefault();
        }

        private void InitCardList() {
            for (int i=0;i<3;i++){
                Card.Card dummyCard = ScriptableObject.CreateInstance<Card.Card>();
                dummyCard.cardName = "Dummy Card" + i;
                dummyCard.description = "This is a dummy card description.";
                dummyCard.cardImage = Resources.Load<Sprite>("dummy");
                availableCard.Add(dummyCard);
            }
        }
    }
}
