using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Manager;
using Script; // Assuming your AchievementManager is under Manager namespace

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using Script; // Assuming your AchievementManager is under Manager namespace


public class PlayerManager
{
    private int enemyKillCounter = 0;

    public int Kill
    {
        get => enemyKillCounter;
        set => enemyKillCounter = value;
    }
    
    public int Planted
    {
        get => plantedPlants;
        set => plantedPlants = value;
    }
    
    private int fullyUpgrade = 0;
    public int FullyUpgrade
    {
        get => fullyUpgrade;
        set => fullyUpgrade = value;
    }
    
    private int upgradedPlants = 0;
    public int Upgraded
    {
        get => upgradedPlants;
        set => upgradedPlants = value;
    }
    
    private int die = 0;
    public int Die
    {
        get => die;
        set => die = value;
    }

    private GameState _gameState;

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
    private int enemyExplodeCounter = 0;
    
    // Resources
    private int collectResourceCounter = 0;
    
    // Special Challenges
    private int sacrificeCounter = 0;
    
    private int firstDie = 0;
    public int tutorialCompleted = 0;
    private AchievementManager achievementManager  = new();
    
    // Remake
    private int rafflesiaDmg = 0;
    private int plantDie = 0;
    private int plantHeal = 0;
    private int plantAlocure = 0;
    private int plantcocoWall = 0;
    
    
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

    public void OnEnemyExploded(int amount)
    {
        killEnemyExplosiveCounter += amount;
        CheckKillEnemyAchievements();
    }

    public void OnEnemyExplode()
    {
        enemyExplodeCounter++;
        CheckEnemyExplodeAchievements();
    }

    public void OnResourceCollect(int amount)
    {
        collectResourceCounter += amount;
        CheckResourceAchievements();
    }

    public void OnPlantSacrifice()
    {
        sacrificeCounter++;
        CheckSpecialChallengesAchievements();
    }

    public void OnRafflesiaDamage(int amount)
    {
        rafflesiaDmg += amount;
        if(rafflesiaDmg >= 1000) achievementManager.UnlockAchievement(EAchievement.TauntMaster);
    }

    public void OnPlantDie()
    {
        plantDie++;
        if(plantDie >= 500) achievementManager.UnlockAchievement(EAchievement.NaturesAvatar);
    }

    public void OnPlantHeal(int amt)
    {
        plantHeal += amt;
        if(plantHeal >= 500) achievementManager.UnlockAchievement(EAchievement.GreenThumb);
    }

    public void OnSurviveAchievement(EAchievement achievement)
    {
        if (
            achievement != EAchievement.SurvivalNotice &&
            achievement != EAchievement.Survivalist &&
            achievement != EAchievement.EnduranceExpert &&
            achievement != EAchievement.BareMinimum
        ) return;
        achievementManager.UnlockAchievement(achievement);
    }

    public void OnPlantAlocure()
    {
        plantAlocure++;
        if(plantAlocure == 100) achievementManager.UnlockAchievement(EAchievement.ZenMaster);
    }

    public void OnPlantCocoWall()
    {
        plantcocoWall++;
        if(plantcocoWall == 100) achievementManager.UnlockAchievement(EAchievement.EcoWarrior);
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

    private bool CheckUnlockAllAchievement()
    {
        return SingletonGame.Instance.AchievementManager.UnlockedEAchievements.Count == 49;
    }
    
    private void CheckSpecialChallengesAchievements()
    {
        // Plant Sacrificer: Sacrifice/Kill 5 of your plants
        if (sacrificeCounter == 5)
        {
            achievementManager.UnlockAchievement(EAchievement.PlantSacrifice);
        }
        // Perfectionist: Achieve 100% completion in the game
        if (CheckUnlockAllAchievement())
        {
            achievementManager.UnlockAchievement(EAchievement.Perfectionist);
        }
    }
    
    private void CheckEnemyExplodeAchievements()
    {
        // Explosive Expertise: Explode 5 enemies
        if (enemyExplodeCounter == 5)
        {
            achievementManager.UnlockAchievement(EAchievement.ExplosiveExpertise);
        }
    }

    private void CheckResourceAchievements()
    {
        // Resource Collector: Collect 15 resources
        if (collectResourceCounter == 15)
        {
            achievementManager.UnlockAchievement(EAchievement.ResourceCollector);
        }

        // Resource Hoarder: Collect 30 resources
        if (collectResourceCounter == 30)
        {
            achievementManager.UnlockAchievement(EAchievement.ResourceHoarder);
        }

        // Herbal Harvester: Collect 50 resources
        if (collectResourceCounter == 50)
        {
            achievementManager.UnlockAchievement(EAchievement.HerbalHarvester);
        }

        // Resource Tycoon: Collect 1000 resources
        if (collectResourceCounter == 1000)
        {
            achievementManager.UnlockAchievement(EAchievement.ResourceTycoon);
        }

        // Resourceful Mind: Collect 5000 resources in total
        if (collectResourceCounter == 5000)
        {
            achievementManager.UnlockAchievement(EAchievement.ResourcefulMind);
        }
    }

    private void CheckKillEnemyAchievements()
    {
        // First Blood: Kill your first enemy
        if (killEnemyCounter == 1)
        {
            achievementManager.UnlockAchievement(EAchievement.FirstBlood);
        }

        // Killer Seed: Kill 10 enemies
        if (killEnemyCounter == 10)
        {
            achievementManager.UnlockAchievement(EAchievement.KillerSeed);
        }

        // Efficient Killer: Kill 50 enemies in 5 minutes
        if (GetEnemyKilledInLast5Minutes() >= 50)
        {   
            achievementManager.UnlockAchievement(EAchievement.EfficientKiller);
        }

        // Monster Slayer: Kill 500 enemies
        if (killEnemyCounter == 500)
        {
            achievementManager.UnlockAchievement(EAchievement.MonsterSlayer);
        }

        // Unstoppable Force: Defeat 1000 enemies in total
        if (killEnemyCounter == 1000)
        {
            achievementManager.UnlockAchievement(EAchievement.UnstoppableForce);
        }

        // Endless Onslaught: Kill 5000 enemies in total
        if (killEnemyCounter == 5000)
        {
            achievementManager.UnlockAchievement(EAchievement.EndlessOnslaught);
        }

        // Monster Frenzy: Kill 200 enemies in under 10 minutes
        if (GetEnemyKilledInLast10Minutes() >= 200)
        {
            achievementManager.UnlockAchievement(EAchievement.MonsterFrenzy);
        }

        // Doomsday Gardener: Kill 200 enemies using explosive plants
        if (killEnemyExplosiveCounter == 200)
        {
            achievementManager.UnlockAchievement(EAchievement.DoomsdayGardener);
        }

        // Trap Specialist: Kill 50 enemies with Boomkin
        if (killEnemyExplosiveCounter == 50)
        {
            achievementManager.UnlockAchievement(EAchievement.TrapSpecialist);
        }
    }

    private void CheckUpgradePlantAchievements()
    {
        // Upgrade Apprentice: Upgrade at least 3 plants
        if (upgradePlantCounter == 3)
        {
            achievementManager.UnlockAchievement(EAchievement.UpgradeApprentice);
        }

        // Upgrade Master: Upgrade at least 10 plants
        if (upgradePlantCounter == 10)
        {
            achievementManager.UnlockAchievement(EAchievement.UpgradeMaster);
        }

        // Upgrade Overachiever: Upgrade at least 30 plants
        if (upgradePlantCounter == 30)
        {
            achievementManager.UnlockAchievement(EAchievement.UpgradeOverachiever);
        }

        // Fully Bloomed: Fully upgrade any plant
        if (fullUpgradePlantCounter >= 1)
        {
            achievementManager.UnlockAchievement(EAchievement.FullyBloomed);
        }

        // Gardener's Glory: Achieve 10 upgraded plants
        if (fullUpgradePlantCounter >= 10)
        {
            achievementManager.UnlockAchievement(EAchievement.GardenersGlory);
        }
    }

    private void CheckLevelUpAchievements()
    {
        // Quick Learner: Level up 3 times in under 1 minute
        if (GetLevelUpInLast1Minutes() == 3)
        {
            achievementManager.UnlockAchievement(EAchievement.QuickLearner);
        }

        // Plant Potential: Level up 5 times
        if (levelupCounter == 5)
        {
            achievementManager.UnlockAchievement(EAchievement.PlantPotential);
        }

        // Level Up Enthusiast: Level up 20 times
        if (levelupCounter == 20)
        {
            achievementManager.UnlockAchievement(EAchievement.LevelUpEnthusiast);
        }

        // Level Up Veteran: Level up 50 times
        if (levelupCounter == 50)
        {
            achievementManager.UnlockAchievement(EAchievement.LevelUpVeteran);
        }
    }

    private void CheckPlantAchievements()
    {
        // New Gardener: Plant 2 plants
        if (plantedPlants == 2)
        {
            achievementManager.UnlockAchievement(EAchievement.NewGardener);
        }

        // Bloom Booster: Plant 20 plants
        if (plantedPlants == 20)
        {
            achievementManager.UnlockAchievement(EAchievement.BloomBooster);
        }

        // Gardening Guru: Plant 50 plants
        if (plantedPlants == 50)
        {
            achievementManager.UnlockAchievement(EAchievement.GardeningGuru);
        }

        // Perfect Planter: Plant 100 plants
        if (plantedPlants == 100)
        {
            achievementManager.UnlockAchievement(EAchievement.PerfectPlanter);
        }

        // Plant Invasion: Plant 1000 plants in total
        if (plantedPlants == 1000)
        {
            achievementManager.UnlockAchievement(EAchievement.PlantInvasion);
        }

        // Master Gardener: Plant 5000 plants in total
        if (plantedPlants == 5000)
        {
            achievementManager.UnlockAchievement(EAchievement.MasterGardener);
        }

        // Speed Planter: Plant 10 plants in 1 minute
        if (GetPlantsPlantedInLast1Minutes() == 10)
        {
            achievementManager.UnlockAchievement(EAchievement.SpeedPlanter);
        }

        // Frenzied Farmer: Plant 50 plants in under 5 minutes
        if (GetPlantsPlantedInLast5Minutes() == 50)
        {
            achievementManager.UnlockAchievement(EAchievement.FrenziedFarmer);
        }

        // Plant Commander: Have 50 plants on the field at once
        if (activePlants == 50)
        {
            achievementManager.UnlockAchievement(EAchievement.PlantCommander);
        }

        // Plant Hoarder: Have 100 plants on the field at once
        if (activePlants == 100)
        {
            achievementManager.UnlockAchievement(EAchievement.PlantHoarder);
        }
    }   

    private void CheckDeathAchievements()
    {
        // First Fall: Die for the first time
        if (firstDie == 1)
        {
            achievementManager.UnlockAchievement(EAchievement.FirstFall);
        }
    }

}
