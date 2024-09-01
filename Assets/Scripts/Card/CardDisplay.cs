using TMPro;
using UnityEngine;

namespace Card
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

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
