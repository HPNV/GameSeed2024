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
    
    // Upgrade Plant category
    private int upgradePlantCounter = 0;
    private int fullUpgradePlantCounter = 0;
    
    // Kill enemy category
    private int killEnemyCounter = 0;
    private List<DateTime> killEnemyTimeStamps = new();
    private int killEnemyExplosiveCounter = 0;
    
    // Explosive
    
    
    private int firstDie = 0;
    public int tutorialCompleted = 0;
    private AchievementManager achievementManager;

    private void Start()
    {
        achievementManager = GetComponent<AchievementManager>();
        
    }

    public void OnPlantPlanted()
    {
        plantedPlants++;

        plantTimeStamps.Add(DateTime.Now);
        
        CheckPlantAchievements();
    }

    public void OnPlantFullyUpgraded()
    {
        fullUpgradePlantCounter++;
        CheckUpgradePlantAchievements();
    }

    public void OnPlantUpgraded()
    {
        upgradePlantCounter++;
        CheckUpgradePlantAchievements();
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
        CheckLevelUpAchievements();
    }

    public void OnEnemyKill()
    {
        killEnemyCounter++;
        killEnemyTimeStamps.Add(DateTime.Now);
        CheckKillEnemyAchievements();
    }

    public void OnEnemyExploded()
    {
        killEnemyExplosiveCounter++;
        CheckKillEnemyAchievements();
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

    private int GetEnemyKilledInLast5Minutes()
    {
        var now = DateTime.Now;
        return killEnemyTimeStamps.Select(t => (now - t).TotalMinutes <= 5).ToList().Count;
    }

    private int GetEnemyKilledInLast10Minutes()
    {
        var now = DateTime.Now;
        killEnemyTimeStamps.RemoveAll(t => (now - t).TotalMinutes > 10);
        return killEnemyTimeStamps.Count;
    }
    
    private void CheckKillEnemyAchievements()
    {
        // First Blood: Kill your first enemy
        if (killEnemyCounter == 1)
        {
            achievementManager.UnlockAchievement("First Blood");
        }

        // Killer Seed: Kill 10 enemies
        if (killEnemyCounter == 10)
        {
            achievementManager.UnlockAchievement("Killer Seed");
        }

        // Efficient Killer: Kill 50 enemies in 5 minutes
        if (GetEnemyKilledInLast5Minutes() >= 50)
        {
            achievementManager.UnlockAchievement("Efficient Killer");
        }

        // Monster Slayer: Kill 500 enemies
        if (killEnemyCounter == 500)
        {
            achievementManager.UnlockAchievement("Monster Slayer");
        }

        // Unstoppable Force: Defeat 1000 enemies in total
        if (killEnemyCounter == 1000)
        {
            achievementManager.UnlockAchievement("Unstoppable Force");
        }

        // Endless Onslaught: Kill 5000 enemies in total
        if (killEnemyCounter == 5000)
        {
            achievementManager.UnlockAchievement("Endless Onslaught");
        }

        // Monster Frenzy: Kill 200 enemies in under 10 minutes
        if (GetEnemyKilledInLast10Minutes() >= 200)
        {
            achievementManager.UnlockAchievement("Monster Frenzy");
        }

        // Doomsday Gardener: Kill 200 enemies using explosive plants
        if (killEnemyExplosiveCounter == 200)
        {
            achievementManager.UnlockAchievement("Doomsday Gardener");
        }

        // Trap Specialist: Kill 50 enemies with Boomkin
        if (killEnemyExplosiveCounter == 50)
        {
            achievementManager.UnlockAchievement("Trap Specialist");
        }
    }
    
    private void CheckUpgradePlantAchievements()
    {
        // Upgrade Apprentice: Upgrade at least 3 plants
        if (upgradePlantCounter == 3)
        {
            achievementManager.UnlockAchievement("Upgrade Apprentice");
        }

        // Upgrade Master: Upgrade at least 10 plants
        if (upgradePlantCounter == 10)
        {
            achievementManager.UnlockAchievement("Upgrade Master");
        }

        // Upgrade Overachiever: Upgrade at least 30 plants
        if (upgradePlantCounter == 30)
        {
            achievementManager.UnlockAchievement("Upgrade Overachiever");
        }

        // Fully Bloomed: Fully upgrade any plant
        if (fullUpgradePlantCounter >= 1)
        {
            achievementManager.UnlockAchievement("Fully Bloomed");
        }

        // Gardener's Glory: Achieve 10 upgraded plants
        if (fullUpgradePlantCounter >= 10)
        {
            achievementManager.UnlockAchievement("Gardener's Glory");
        }
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
