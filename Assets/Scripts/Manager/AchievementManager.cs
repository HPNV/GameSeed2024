﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Achievement;
using Script;
using UnityEngine;

namespace Manager
{
    public class AchievementManager : MonoBehaviour
    {
        [SerializeField]
        private List<EAchievement> enums;
        [SerializeField]
        private List<AchievementData> data;
        public Dictionary<EAchievement, AchievementData> Achievements = new();
        public List<EAchievement> UnlockedEAchievements { get; private set; }

        private void Start()
        {
            UnlockedEAchievements = new List<EAchievement>();
            Achievements = new Dictionary<EAchievement, AchievementData>();
            for (var i = 0; i < enums.Count; i++)
            {
                Achievements[enums[i]] = data[i];
            }
        }

        public void UnlockAchievement(EAchievement achievement)
        {
            if (UnlockedEAchievements.Contains(achievement))
            {
                return;
            }
            
            PlayerManager.Instance.UnlockedAchievements += 1;
            UnlockedEAchievements.Add(achievement);
            Debug.Log("Achievement Unlocked: " + Achievements[achievement].name);
            ShowAchievement(achievement);
            
            CheckUnlockAllAchievement();
            CheckUnlock8Plants();
        }

        private void CheckUnlockAllAchievement()
        {
            if (UnlockedEAchievements.Count == 49) UnlockAchievement(EAchievement.UltimateGardener);
        }

        private void CheckUnlock8Plants()
        {
            if (UnlockedEAchievements.Count == 10) UnlockAchievement(EAchievement.PlantCollector);
        }

        public IEnumerable<EAchievement> GetRandomEAchievements(int amt)
        {
            var temp = UnlockedEAchievements.OrderBy(x => Guid.NewGuid());
            return temp.Take(amt);
        }

        public IEnumerator ShowAchievement(EAchievement achievement)
        {
            SingletonGame.Instance.AchivementPrefab.SetAchievement(Achievements[achievement].achievementImage, Achievements[achievement].name);
            SingletonGame.Instance.AchivementPrefab.gameObject.SetActive(true);
            yield return new WaitForSeconds(3);
            SingletonGame.Instance.AchivementPrefab.gameObject.SetActive(false);
        }
    }
}