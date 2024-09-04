using UnityEngine;

namespace Achievement
{
    [CreateAssetMenu(fileName = "New Achievement", menuName = "Data/Achievement Data")]
    public class AchievementData : ScriptableObject
    {
        public string achievementName = "Dep";
        public string description = "Dep";
        public Sprite achievementImage;
        public string plantType = "Dep";
    }
}