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
        public TextMeshProUGUI cardNameText;
        public Sprite cardImageHolder;
        public TextMeshProUGUI descriptionText;
        public TextMeshProUGUI attackText;
        public TextMeshProUGUI healthText;
        public TextMeshProUGUI attackSpeedText;
        public TextMeshProUGUI rangeText;
        public SpriteRenderer spriteRenderer;
        void Start()
        {
            SetCard(cardData);
        }

        void Update()
        {
            
        }

        public void SetCard(PlantData plantData) 
        {
            cardData = plantData;
            cardNameText.text = plantData.plantType.ToString();
            descriptionText.text = plantData.description;
            cardImageHolder = plantData.sprite;
            spriteRenderer.sprite = plantData.sprite;
            attackText.text = plantData.damage.ToString();
            healthText.text = plantData.health.ToString();
            attackSpeedText.text = plantData.attackCooldown.ToString();
            rangeText.text = plantData.range.ToString();
        }

        public void OnMouseDown() {
            SingletonGame.Instance.PickCard(cardData);
            Debug.Log("Mouse Down");
        }
    }
}
