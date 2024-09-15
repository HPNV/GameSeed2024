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
using Script;
using UnityEngine.Serialization; // Assuming your AchievementManager is under Manager namespace


public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
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

    private bool completeTutorial = false;
    public bool CompleteTutorial
    {
        get => completeTutorial;
        set => completeTutorial = value;
    }

    private GameState _gameState;

    // Planting category
    private int plantedPlants = 0;

    public int PlantedPlants
    {
        get => plantedPlants;
        set => plantedPlants = value;
    }
    
    private List<DateTime> plantTimeStamps = new();
    private int activePlants = 0;
    
    // Level up category
    private List<DateTime> levelupTimeStamps = new();
    private int levelupCounter = 0;
    public int LevelUpCounter
    {
        get => levelupCounter;
        set => levelupCounter = value;
    }
    
    // Upgrade Plant category
    private int upgradePlantCounter = 0;
    public int UpgradePlantCounter
    {
        get => upgradePlantCounter;
        set => upgradePlantCounter = value;
    }
    private int fullUpgradePlantCounter = 0;
    public int FullUpgradePlantCounter
    {
        get => fullUpgradePlantCounter;
        set => fullUpgradePlantCounter = value;
    }
    
    public List<bool> SurvivalData { get; set; } = new() { false, false, false, false, false, false };
    public List<bool> ActivePlantData { get; set; } = new() { false, false };
    public List<bool> ExplosiveData { get; set; } = new() { false };
    public List<bool> PlantedInTimeData { get; set; } = new() { false, false };
    public List<bool> UtilsData { get; set; } = new() { false, false, false, false, false, false, false };
    
    public int UnlockedAchievements { get; set; } = 0;
    // Kill enemy category
    private int killEnemyCounter = 0;
    private List<DateTime> killEnemyTimeStamps = new();
    private int killEnemyExplosiveCounter = 0;
    
    // Explosive
    private int enemyExplodeCounter = 0;
    public int EnemyExplodeCounter
    {
        get => enemyExplodeCounter;
        set => enemyExplodeCounter = value;
    }
    
    // Resources
    private int collectResourceCounter = 0;
    public int CollectResourceCounter
    {
        get => collectResourceCounter;
        set => collectResourceCounter = value;
    }
    
    // Special Challenges
    private int sacrificeCounter = 0;
    public int SacrificeCounter
    {
        get => sacrificeCounter;
        set => sacrificeCounter = value;
    }
    
    private int firstDie = 0;

    public int Die
    {
        get => firstDie;
        set => firstDie = value;
    }
    
    public int tutorialCompleted = 0;
    public AchievementManager AchievementManager;
    
    // Remake
    private int rafflesiaDmg = 0;
    private int plantDie = 0;
    private int plantHeal = 0;
    private int plantAlocure = 0;
    private int plantcocoWall = 0;
    public DateTime LastHitTimeStamp = new();
    private int plantPlant = 0;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            Initialize();
            // DontDestroyOnLoad(gameObject);
        }
        // else
        // {   
        //     Destroy(gameObject);
        // }
    }

    private void Initialize()
    {
        AchievementManager = AchievementManager.Instance;
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
        if (sacrificeCounter == 5) AchievementManager.UnlockAchievement(EAchievement.PlantSacrifice);
        else if (sacrificeCounter == 5000) AchievementManager.UnlockAchievement(EAchievement.AgainstAllOdds);
    }

    public void OnRafflesiaDamage(float amount)
    {
        rafflesiaDmg += (int)amount;
        if (rafflesiaDmg >= 1000)
        {
            UtilsData[0] = true;
            AchievementManager.UnlockAchievement(EAchievement.TauntMaster);
        }
    }

    public void OnPlantDie()
    {
        plantDie++;
        if (plantDie >= 500)
        {
            UtilsData[1] = true;
            AchievementManager.UnlockAchievement(EAchievement.NaturesAvatar);
        }
    }

    public void OnPlantHeal(int amt)
    {
        plantHeal += amt;
        if (plantHeal >= 500)
        {
            UtilsData[2] = true;
            AchievementManager.UnlockAchievement(EAchievement.GreenThumb);
        }
    }

    public void OnSurviveAchievement(EAchievement achievement)
    {
        var survivalList = new List<EAchievement>
        {
            EAchievement.SurvivalNotice,
            EAchievement.Survivalist,
            EAchievement.EnduranceExpert,
            EAchievement.BareMinimum,
            EAchievement.Untouchable,
            EAchievement.FlawlessDefense
        };
        if (!survivalList.Contains(achievement)) 
            return;
        
        SurvivalData[survivalList.IndexOf(achievement)] = true;
        AchievementManager.UnlockAchievement(achievement);
    }

    public void OnPlantAlocure()
    {
        plantAlocure++;
        if (plantAlocure == 100)
        {
            UtilsData[5] = true;
            AchievementManager.UnlockAchievement(EAchievement.ZenMaster);
        }
    }

    public void OnPlantCocoWall()
    {
        plantcocoWall++;
        if (plantcocoWall == 100)
        {
            UtilsData[6] = true;
            AchievementManager.UnlockAchievement(EAchievement.EcoWarrior);
        }
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
    
    private void CheckEnemyExplodeAchievements()
    {
        // Explosive Expertise: Explode 5 enemies
        if (enemyExplodeCounter == 5)
        {
            ExplosiveData[0] = true;
            AchievementManager.UnlockAchievement(EAchievement.ExplosiveExpertise);
        }
    }

    private void CheckResourceAchievements()
    {
        // Resource Collector: Collect 15 resources
        if (collectResourceCounter == 15)
        {
            AchievementManager.UnlockAchievement(EAchievement.ResourceCollector);
        }

        // Resource Hoarder: Collect 30 resources
        if (collectResourceCounter == 30)
        {
            AchievementManager.UnlockAchievement(EAchievement.ResourceHoarder);
        }

        // Herbal Harvester: Collect 50 resources
        if (collectResourceCounter == 50)
        {
            AchievementManager.UnlockAchievement(EAchievement.HerbalHarvester);
        }

        // Resource Tycoon: Collect 1000 resources
        if (collectResourceCounter == 1000)
        {
            AchievementManager.UnlockAchievement(EAchievement.ResourceTycoon);
        }

        // Resourceful Mind: Collect 5000 resources in total
        if (collectResourceCounter == 5000)
        {
            AchievementManager.UnlockAchievement(EAchievement.ResourcefulMind);
        }
    }

    private void CheckKillEnemyAchievements()
    {
        // First Blood: Kill your first enemy
        if (killEnemyCounter == 1)
        {
            AchievementManager.UnlockAchievement(EAchievement.FirstBlood);
        }

        // Killer Seed: Kill 10 enemies
        if (killEnemyCounter == 10)
        {
            AchievementManager.UnlockAchievement(EAchievement.KillerSeed);
        }

        // Efficient Killer: Kill 50 enemies in 5 minutes
        if (GetEnemyKilledInLast5Minutes() >= 50)
        {   
            AchievementManager.UnlockAchievement(EAchievement.EfficientKiller);
        }

        // Monster Slayer: Kill 500 enemies
        if (killEnemyCounter == 500)
        {
            AchievementManager.UnlockAchievement(EAchievement.MonsterSlayer);
        }

        // Unstoppable Force: Defeat 1000 enemies in total
        if (killEnemyCounter == 1000)
        {
            AchievementManager.UnlockAchievement(EAchievement.UnstoppableForce);
        }

        // Endless Onslaught: Kill 5000 enemies in total
        if (killEnemyCounter == 5000)
        {
            AchievementManager.UnlockAchievement(EAchievement.EndlessOnslaught);
        }

        // Monster Frenzy: Kill 200 enemies in under 10 minutes
        if (GetEnemyKilledInLast10Minutes() >= 200)
        {
            AchievementManager.UnlockAchievement(EAchievement.MonsterFrenzy);
        }

        // Doomsday Gardener: Kill 200 enemies using explosive plants
        if (killEnemyExplosiveCounter == 200)
        {
            UtilsData[3] = true;
            AchievementManager.UnlockAchievement(EAchievement.DoomsdayGardener);
        }

        // Trap Specialist: Kill 50 enemies with Boomkin
        if (killEnemyExplosiveCounter == 50)
        {
            UtilsData[4] = true;
            AchievementManager.UnlockAchievement(EAchievement.TrapSpecialist);
        }
    }

    private void CheckUpgradePlantAchievements()
    {
        // Upgrade Apprentice: Upgrade at least 3 plants
        if (upgradePlantCounter == 3)
        {
            AchievementManager.UnlockAchievement(EAchievement.UpgradeApprentice);
        }

        // Upgrade Master: Upgrade at least 10 plants
        if (upgradePlantCounter == 10)
        {
            AchievementManager.UnlockAchievement(EAchievement.UpgradeMaster);
        }

        // Upgrade Overachiever: Upgrade at least 30 plants
        if (upgradePlantCounter == 30)
        {
            AchievementManager.UnlockAchievement(EAchievement.UpgradeOverachiever);
        }

        // Fully Bloomed: Fully upgrade any plant
        if (fullUpgradePlantCounter >= 1)
        {
            AchievementManager.UnlockAchievement(EAchievement.FullyBloomed);
        }

        // Gardener's Glory: Achieve 10 upgraded plants
        if (fullUpgradePlantCounter >= 10)
        {
            AchievementManager.UnlockAchievement(EAchievement.GardenersGlory);
        }
    }

    private void CheckLevelUpAchievements()
    {
        // Quick Learner: Level up 3 times in under 1 minute
        if (GetLevelUpInLast1Minutes() == 3)
        {
            AchievementManager.UnlockAchievement(EAchievement.QuickLearner);
        }

        // Plant Potential: Level up 5 times
        if (levelupCounter == 5)
        {
            AchievementManager.UnlockAchievement(EAchievement.PlantPotential);
        }

        // Level Up Enthusiast: Level up 20 times
        if (levelupCounter == 20)
        {
            AchievementManager.UnlockAchievement(EAchievement.LevelUpEnthusiast);
        }

        // Level Up Veteran: Level up 50 times
        if (levelupCounter == 50)
        {
            AchievementManager.UnlockAchievement(EAchievement.LevelUpVeteran);
        }
    }

    private void CheckPlantAchievements()
    {
        // New Gardener: Plant 2 plants
        if (plantedPlants == 2)
        {
            AchievementManager.UnlockAchievement(EAchievement.NewGardener);
        }

        // Bloom Booster: Plant 20 plants
        if (plantedPlants == 20)
        {
            AchievementManager.UnlockAchievement(EAchievement.BloomBooster);
        }

        // Gardening Guru: Plant 50 plants
        if (plantedPlants == 50)
        {
            AchievementManager.UnlockAchievement(EAchievement.GardeningGuru);
        }

        // Perfect Planter: Plant 100 plants
        if (plantedPlants == 100)
        {
            AchievementManager.UnlockAchievement(EAchievement.PerfectPlanter);
        }

        // Plant Invasion: Plant 1000 plants in total
        if (plantedPlants == 1000)
        {
            AchievementManager.UnlockAchievement(EAchievement.PlantInvasion);
        }
        
        // Botanical Diversity: Plant 5000 plants
        if (plantedPlants == 5000)
        {
            AchievementManager.UnlockAchievement(EAchievement.BotanicalDiversity);
        }
        
        // Master Gardener: Plant 10000 plants in total
        if (plantedPlants == 10000)
        {
            AchievementManager.UnlockAchievement(EAchievement.MasterGardener);
        }

        // Speed Planter: Plant 10 plants in 1 minute
        if (GetPlantsPlantedInLast1Minutes() == 10)
        {
            AchievementManager.UnlockAchievement(EAchievement.SpeedPlanter);
        }

        // Frenzied Farmer: Plant 50 plants in under 5 minutes
        if (GetPlantsPlantedInLast5Minutes() == 50)
        {
            AchievementManager.UnlockAchievement(EAchievement.FrenziedFarmer);
        }

        // Plant Commander: Have 50 plants on the field at once
        if (activePlants == 50)
        {
            ActivePlantData[0] = true;
            AchievementManager.UnlockAchievement(EAchievement.PlantCommander);
        }

        // Plant Hoarder: Have 100 plants on the field at once
        if (activePlants == 100)
        {
            ActivePlantData[1] = true;
            AchievementManager.UnlockAchievement(EAchievement.PlantHoarder);
        }
    }   

    private void CheckDeathAchievements()
    {
        // First Fall: Die for the first time
        if (firstDie == 1)
        {
            AchievementManager.UnlockAchievement(EAchievement.FirstFall);
        }
    }

}
