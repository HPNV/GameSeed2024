using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Utils;

namespace CardClass
{
        public class CardDisplay : MonoBehaviour
        {
            public Card card;
            public TextMeshPro cardNameText;
            public Sprite cardImageHolder;
            public TextMeshPro descriptionText;
            void Start()
            {
                cardNameText.text = card.cardName;
                descriptionText.text = card.description;
                cardImageHolder = card.cardImage;   
            }

        void Update()
        {
            
        }

        public void setCard(Card card) {
            this.card = card;
            cardNameText.text = card.cardName;
            descriptionText.text = card.description;
            cardImageHolder = card.cardImage;
        }

        public void OnMouseDown() {
            SingletonGame.Instance.PickCard(card);
        }
    }
}
