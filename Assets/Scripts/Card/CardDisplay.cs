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
        public EPlant plantType;
        public PlantData cardData;
        public TextMeshPro cardNameText;
        public Sprite cardImageHolder;
        public TextMeshPro descriptionText;
        public TextMeshPro attackText;
        public TextMeshPro healthText;
        public TextMeshPro attackSpeedText;
        public TextMeshPro rangeText;
        void Start()
        {

        }

        void Update()
        {
            
        }

        public void SetCard(PlantData cardData, EPlant plantType) {
            this.cardData = cardData;
            this.plantType = plantType;
            cardNameText.text = cardData.plantName;
            descriptionText.text = cardData.description;
            cardImageHolder = cardData.sprite;
            attackText.text = cardData.damage.ToString();
            healthText.text = cardData.health.ToString();
            attackSpeedText.text = cardData.cd.ToString();
            rangeText.text = cardData.range.ToString();
        }

        public void OnMouseDown() {
            SingletonGame.Instance.PickCard(plantType);
            Debug.Log("Mouse Down");
        }
    }
}
