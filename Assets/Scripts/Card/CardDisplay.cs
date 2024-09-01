using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

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

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
