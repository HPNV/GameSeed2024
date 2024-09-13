using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Manager;
using Script; // Assuming your AchievementManager is under Manager namespace

public class PlayerManager : MonoBehaviour
{
    // Planting category
    private int plantedPlants = 0;
    private List<DateTime> plantTimeStamps = new();
    private int activePlants = 0;
    
    // Level up category
    private List<DateTime> levelupTimeStamps = new();
    private int levelupCounter = 0;
    
    private int enemyKillCounter = 0;
    private int fullyUpgrade = 0;
    private int upgradedPlants = 0;
    private int firstDie = 0;
    public int tutorialCompleted = 0;
    private AchievementManager achievementManager;

    private void Start()
    {
        achievementManager = GetComponent<AchievementManager>();
        
    }

    public void OnEnemyKilled()
    {
        enemyKillCounter++;
        CheckKillAchievements();
    }

    public void OnPlantPlanted()
    {
        plantedPlants++;

        plantTimeStamps.Add(DateTime.Now);
        
        CheckPlantAchievements();
    }

    public void OnPlantFullyUpgraded()
    {
        fullyUpgrade++;
        CheckUpgradeAchievements();
    }

    public void OnPlantUpgraded()
    {
        upgradedPlants++;
        CheckUpgradeAchievements();
    }

    public void OnPlayerDied()
    {
        firstDie++;
        CheckDeathAchievements();
    }

    public void OnPlayerLevelUp()
    {
        levelupCounter++;
        levelupTimeStamps.Add(DateTime.Now);
    }
    
    private int GetPlantsPlantedInLast5Minutes()
    {
        var now = DateTime.Now;
        plantTimeStamps.RemoveAll(timestamp => (now - timestamp).TotalMinutes > 5);
        return plantTimeStamps.Count;
    }
    
    private int GetPlantsPlantedInLast1Minutes()
    {
        var now = DateTime.Now;
        plantTimeStamps.RemoveAll(timestamp => (now - timestamp).TotalMinutes > 5);
        return plantTimeStamps.Where(t => (now - t).TotalMinutes <= 1).ToList().Count;
    }

    private int GetLevelUpInLast1Minutes()
    {
        var now = DateTime.Now;
        levelupTimeStamps.RemoveAll(t => (now - t).TotalMinutes > 1);
        return levelupTimeStamps.Count;
    }

    private void CheckLevelUpAchievements()
    {
        // Quick Learner: Level up 3 times in under 1 minute
        if (GetLevelUpInLast1Minutes() == 3)
        {
            achievementManager.UnlockAchievement("Quick Learner");
        }

        // Plant Potential: Level up 5 times
        if (levelupCounter == 5)
        {
            achievementManager.UnlockAchievement("Plant Potential");
        }

        // Level Up Enthusiast: Level up 20 times
        if (levelupCounter == 20)
        {
            achievementManager.UnlockAchievement("Level Up Enthusiast");
        }

        // Level Up Veteran: Level up 50 times
        if (levelupCounter == 50)
        {
            achievementManager.UnlockAchievement("Level Up Veteran");
        }
    }
    
    private void CheckKillAchievements()
    {
        //TEMP
        if (achievementManager == null)
            return;
        // First Blood: Kill your first enemy
        if (enemyKillCounter == 1)
        {
            achievementManager.UnlockAchievement("First Blood");
        }

        // Killer Seed: Kill 10 enemies
        if (enemyKillCounter == 10)
        {
            achievementManager.UnlockAchievement("Killer Seed");
        }

        // Monster Slayer: Kill 500 enemies
        if (enemyKillCounter == 500)
        {
            achievementManager.UnlockAchievement("Monster Slayer");
        }

        // Unstoppable Force: Kill 1000 enemies
        if (enemyKillCounter == 1000)
        {
            achievementManager.UnlockAchievement("Unstoppable Force");
        }
    }

    private void CheckPlantAchievements()
    {
        // New Gardener: Plant 2 plants
        if (plantedPlants == 2)
        {
            achievementManager.UnlockAchievement("New Gardener");
        }

        // Bloom Booster: Plant 20 plants
        if (plantedPlants == 20)
        {
            achievementManager.UnlockAchievement("Bloom Booster");
        }

        // Gardening Guru: Plant 50 plants
        if (plantedPlants == 50)
        {
            achievementManager.UnlockAchievement("Gardening Guru");
        }

        // Perfect Planter: Plant 100 plants
        if (plantedPlants == 100)
        {
            achievementManager.UnlockAchievement("Perfect Planter");
        }

        // Plant Invasion: Plant 1000 plants in total
        if (plantedPlants == 1000)
        {
            achievementManager.UnlockAchievement("Plant Invasion");
        }

        // Master Gardener: Plant 5000 plants in total
        if (plantedPlants == 5000)
        {
            achievementManager.UnlockAchievement("Master Gardener");
        }

        // Speed Planter: Plant 10 plants in 1 minute
        if (GetPlantsPlantedInLast1Minutes() == 10)
        {
            achievementManager.UnlockAchievement("Speed Planter");
        }

        // Frenzied Farmer: Plant 50 plants in under 5 minutes
        if (GetPlantsPlantedInLast5Minutes() == 50)
        {
            achievementManager.UnlockAchievement("Frenzied Farmer");
        }

        // Plant Commander: Have 50 plants on the field at once
        if (activePlants == 50)
        {
            achievementManager.UnlockAchievement("Plant Commander");
        }

        // Plant Hoarder: Have 100 plants on the field at once
        if (activePlants == 100)
        {
            achievementManager.UnlockAchievement("Plant Hoarder");
        }
    }

    private void CheckUpgradeAchievements()
    {
        if (achievementManager == null)
            return;
        // Fully Bloomed: Fully upgrade any plant
        if (fullyUpgrade >= 1)
        {
            achievementManager.UnlockAchievement("Fully Bloomed");
        }

        // Upgrade Apprentice: Upgrade at least 3 plants
        if (upgradedPlants >= 3)
        {
            achievementManager.UnlockAchievement("Upgrade Apprentice");
        }

        // Upgrade Master: Upgrade at least 10 plants
        if (upgradedPlants >= 10)
        {
            achievementManager.UnlockAchievement("Upgrade Master");
        }
    }

    private void CheckDeathAchievements()
    {
        if (achievementManager == null)
            return;
        // First Fall: Die for the first time
        if (firstDie == 1)
        {
            achievementManager.UnlockAchievement("First Fall");
        }
    }
}
