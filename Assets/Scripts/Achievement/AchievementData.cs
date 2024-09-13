using Script;
using UnityEngine;

namespace Achievement
{
    [CreateAssetMenu(fileName = "New Achievement", menuName = "Data/Achievement Data")]
    public class AchievementData : ScriptableObject
    {
        public EAchievement achievement;
        public string achievementName;
        public string description;
        public Sprite achievementImage;
    }
}