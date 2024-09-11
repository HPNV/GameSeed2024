using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using Script; // Assuming your AchievementManager is under Manager namespace

public class PlayerManager : MonoBehaviour
{
    private int enemyKillCounter = 0;
    private int plantedPlants = 0;
    private int fullyUpgrade = 0;
    private int upgradedPlants = 0;
    private int firstDie = 0;
    public int tutorialCompleted = 0;  
    private AchievementManager achievementManager;

    

    private void Start()
    {
        // Assuming the AchievementManager is on the same GameObject
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

        // Perfect Planter: Plant 100 plants
        if (plantedPlants == 100)
        {
            achievementManager.UnlockAchievement("Perfect Planter");
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
