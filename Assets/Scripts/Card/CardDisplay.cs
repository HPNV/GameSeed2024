using Plant;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace Card
{
    public class CardDisplay : MonoBehaviour
    {
        public PlantData cardData;
        public TextMeshPro cardNameText;
        public Sprite cardImageHolder;
        public TextMeshPro descriptionText;
        void Start()
        {
            cardNameText.text = cardData.plantName;
            descriptionText.text = "none";
            cardImageHolder = null;   
        }

        void Update()
        {
            
        }

        public void SetCard(PlantData cardData) {
            this.cardData = cardData;
            cardNameText.text = cardData.plantName;
            descriptionText.text = "";
            cardImageHolder = null;
        }

        public void OnMouseDown() {
            SingletonGame.Instance.PickCard(cardData);
            Debug.Log("Mouse Down");
        }
    }
}
