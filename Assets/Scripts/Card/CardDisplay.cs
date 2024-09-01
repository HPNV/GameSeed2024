using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace Card
{
    public class CardDisplay : MonoBehaviour
    {
        [FormerlySerializedAs("card")] public CardData cardData;
        public TextMeshPro cardNameText;
        public Sprite cardImageHolder;
        public TextMeshPro descriptionText;
        void Start()
        {
            cardNameText.text = cardData.cardName;
            descriptionText.text = cardData.description;
            cardImageHolder = cardData.cardImage;   
        }

        void Update()
        {
            
        }

        public void setCard(CardData cardData) {
            this.cardData = cardData;
            cardNameText.text = cardData.cardName;
            descriptionText.text = cardData.description;
            cardImageHolder = cardData.cardImage;
        }

        public void OnMouseDown() {
            SingletonGame.Instance.PickCard(cardData);
        }
    }
}
