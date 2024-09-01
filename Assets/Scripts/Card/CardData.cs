using UnityEngine;

namespace Card
{
    [CreateAssetMenu(fileName = "NewCard", menuName = "Cards/Card")]
    public class CardData : ScriptableObject
    {
        public string cardName;
        public Sprite cardImage;
        public string description;
    }
}
