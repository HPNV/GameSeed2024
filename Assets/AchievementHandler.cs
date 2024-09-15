using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Firebase.Firestore;
using Firebase.Extensions;
using Manager;
using Script;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementHandler : MonoBehaviour
{
    private FirebaseFirestore db;
    [SerializeField] private GameObject seedpediaPanel;
    [SerializeField] private Sprite backgroundSprite;

    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;

        fetchUserData(ReconcileAchievement);
    }

    private void fetchUserData(Action onUserDataFetched = null)
    {
        string hostname = System.Environment.MachineName;
        DocumentReference docRef = db.Collection("users").Document(hostname);

        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                Dictionary<string, object> data = snapshot.ToDictionary();
                
                PlayerManager.Instance.Die = Convert.ToInt32(data["die_counter"]);
                PlayerManager.Instance.Kill = Convert.ToInt32(data["kill_counter"]);
                
                PlayerManager.Instance.UpgradePlantCounter = Convert.ToInt32(data["upgrade_plant_counter"]);
                PlayerManager.Instance.FullUpgradePlantCounter = Convert.ToInt32(data["full_upgrade_plant_counter"]);
                PlayerManager.Instance.CollectResourceCounter = Convert.ToInt32(data["collect_resource_counter"]);
                PlayerManager.Instance.UnlockedAchievements = Convert.ToInt32(data["unlocked_achievements"]);
                PlayerManager.Instance.SacrificeCounter = Convert.ToInt32(data["sacrifice_counter"]);
                PlayerManager.Instance.PlantedPlants = Convert.ToInt32(data["planted_plants_counter"]);
                PlayerManager.Instance.LevelUpCounter = Convert.ToInt32(data["level_up_counter"]);
                PlayerManager.Instance.CompleteTutorial = Convert.ToBoolean(data["complete_tutorial"]);
                PlayerManager.Instance.SurvivalData = data["survival_data"] as List<bool>;
                PlayerManager.Instance.ActivePlantData = data["active_plant_data"] as List<bool>;
                PlayerManager.Instance.ExplosiveData = data["explosive_data"] as List<bool>;
                PlayerManager.Instance.PlantedInTimeData = data["planted_in_time_data"] as List<bool>;
                PlayerManager.Instance.UtilsData = data["utils_data"] as List<bool>;
                Debug.Log("User data fetched successfully.");

                // Trigger the callback or event when data is fetched
                onUserDataFetched?.Invoke();
            }
            else
            {
                Debug.Log("Document " + snapshot.Id + " does not exist!");
            }
        });
    }

    

    private static Dictionary<EAchievement, Dictionary<string, object>> _achievementData = new()
    {
        { EAchievement.NewGardener, new Dictionary<string, object>() { { "counter", 2 } } }, // v
        { EAchievement.KillerSeed, new Dictionary<string, object>() { { "counter", 10 } } }, // v
        { EAchievement.PlantPotential, new Dictionary<string, object>() { { "counter", 5 } } }, // v
        { EAchievement.UpgradeApprentice, new Dictionary<string, object>() { { "counter", 3 } } }, // v
        { EAchievement.FirstFall, new Dictionary<string, object>() { { "counter", 1 } } }, // v
        { EAchievement.BloomBooster, new Dictionary<string, object>() { { "counter", 20 } } }, // v
        { EAchievement.SurvivalNotice, new Dictionary<string, object>() }, // v
        { EAchievement.Survivalist, new Dictionary<string, object>() { } }, // v
        { EAchievement.LevelUpEnthusiast, new Dictionary<string, object>() { { "counter", 20 } } }, // v
        { EAchievement.LevelUpVeteran, new Dictionary<string, object>() { { "counter", 50 } } }, // v
        { EAchievement.GardeningGuru, new Dictionary<string, object>() { { "counter", 50 } } }, // v
        { EAchievement.EnduranceExpert, new Dictionary<string, object>() { } }, // v
        { EAchievement.UpgradeMaster, new Dictionary<string, object>() { { "counter", 10 } } }, // v
        { EAchievement.UpgradeOverachiever, new Dictionary<string, object>() { { "counter", 30 } } }, // v
        { EAchievement.ExplosiveExpertise, new Dictionary<string, object>() { { "counter", 5 } } }, // v
        { EAchievement.ResourceCollector, new Dictionary<string, object>() { { "counter", 15 } } },  // v
        { EAchievement.ResourceHoarder, new Dictionary<string, object>() { { "counter", 30 } } },  // v
        { EAchievement.PlantSacrifice, new Dictionary<string, object>() { { "counter", 5 } } }, // v
        { EAchievement.BotanicalDiversity, new Dictionary<string, object>() { { "counter", 5000 } } }, // v
        { EAchievement.HerbalHarvester, new Dictionary<string, object>() { { "counter", 50 } } },  // v
        { EAchievement.MonsterSlayer, new Dictionary<string, object>() { { "counter", 500 } } }, // v
        { EAchievement.PerfectPlanter, new Dictionary<string, object>() { { "counter", 100 } } }, // v
        { EAchievement.FlawlessDefense, new Dictionary<string, object>() { } }, // v
        { EAchievement.SpeedPlanter, new Dictionary<string, object>() { { "counter", 10 } } },  // v
        { EAchievement.EfficientKiller, new Dictionary<string, object>() { { "counter", 50 } } }, // v
        { EAchievement.TauntMaster, new Dictionary<string, object>() { } },
        { EAchievement.ResourceTycoon, new Dictionary<string, object>() { { "counter", 1000 } } },  // v
        { EAchievement.PlantCollector, new Dictionary<string, object>() { { "counter", 10 }} }, // v
        { EAchievement.UnstoppableForce, new Dictionary<string, object>() { { "counter", 1000 } } }, // v
        { EAchievement.TrapSpecialist, new Dictionary<string, object>() { { "counter", 50 } } },
        { EAchievement.FirstBlood, new Dictionary<string, object>() { { "counter", 1 } } }, // v
        { EAchievement.PlantInvasion, new Dictionary<string, object>() { { "counter", 1000 } } }, // v
        { EAchievement.NaturesAvatar, new Dictionary<string, object>() { } },
        { EAchievement.GreenThumb, new Dictionary<string, object>() { } },
        { EAchievement.MonsterFrenzy, new Dictionary<string, object>() { { "counter", 200 } } }, // v
        { EAchievement.PlantCommander, new Dictionary<string, object>() { { "counter", 50 } } }, // v
        { EAchievement.AgainstAllOdds, new Dictionary<string, object>() { { "counter", 5000 } } }, // v
        { EAchievement.DoomsdayGardener, new Dictionary<string, object>() { { "counter", 200 } } },
        { EAchievement.ZenMaster, new Dictionary<string, object>() { } },
        { EAchievement.EcoWarrior, new Dictionary<string, object>() { } },
        { EAchievement.BareMinimum, new Dictionary<string, object>() { } }, // v
        { EAchievement.PlantHoarder, new Dictionary<string, object>() { { "counter", 100 } } }, // v
        { EAchievement.MasterGardener, new Dictionary<string, object>() { { "counter", 10000 } } }, // v
        { EAchievement.ResourcefulMind, new Dictionary<string, object>() { { "counter", 5000 } } },  // v
        { EAchievement.EndlessOnslaught, new Dictionary<string, object>() { { "counter", 5000 } } }, // v
        { EAchievement.FrenziedFarmer, new Dictionary<string, object>() { { "counter", 50 } } }, // v
        { EAchievement.Untouchable, new Dictionary<string, object>() { } }, // v
        { EAchievement.UltimateGardener, new Dictionary<string, object>() { { "counter", 49 }} }, // v
        { EAchievement.QuickLearner, new Dictionary<string, object>() { { "counter", 3 } } }, // v
        { EAchievement.FullyBloomed, new Dictionary<string, object>() { { "counter", 1 } } }, // v
    };
    
    private void ReconcileAchievement()
    {
        var data = AchievementManager.Instance.Achievements;
        
        // Check if the achievement data exists and is populated
        if (data == null || data.Count == 0)
        {
            Debug.LogError("Achievement data is null or empty.");
            return;
        }

        var keys = data.Keys.ToList();
        int i = 0;
        
        Debug.Log("ReconcileAchievement called. Processing UI...");
        
        // Loop through each child (row) of the seedpediaPanel
        foreach (Transform child in seedpediaPanel.transform)
        {
            // Iterate through buttons in each row (grandchildren of seedpediaPanel)
            foreach (Transform grandchild in child)
            {
                if (i >= keys.Count)
                {
                    Debug.LogWarning("Index out of range: No more achievements to process.");
                    break;
                }

                var selectedKey = keys[i];
                Button button = grandchild.GetComponent<Button>();
                if (button != null)
                {
                    // Set the background sprite for the button
                    button.image.sprite = backgroundSprite;

                    // Get all Image components in the button's children (ignoring the button's own image)
                    Image[] images = button.GetComponentsInChildren<Image>();
                    TextMeshProUGUI[] texts = button.GetComponentsInChildren<TextMeshProUGUI>();
                    Slider[] sliders = button.GetComponentsInChildren<Slider>();

                    if (images.Length > 1)
                    {
                        string plantId = data[selectedKey].name;
                        Debug.Log($"Loading achievement: {plantId}");

                        // Construct the image path for the plant sprite
                        string imagePath = "Images/Achievements/" + plantId.Replace(' ', '_').ToLower();
                        Sprite plantSprite = Resources.Load<Sprite>(imagePath);

                        if (plantSprite != null)
                        {
                            // Set the plant image sprite
                            images[1].sprite = plantSprite;
                            texts[0].text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(plantId.Replace('_', ' '));

                            if (_achievementData[selectedKey].ContainsKey("counter"))
                            {
                                var counter = Convert.ToInt32(_achievementData[selectedKey]["counter"]);
                                var achievementCounter = GetAchievementCounter(selectedKey, counter);
                                sliders[0].maxValue = counter;
                                sliders[0].value = achievementCounter;
                                texts[1].text = achievementCounter + "/" + _achievementData[selectedKey]["counter"];
                            }
                            
                            texts[2].text = data[selectedKey].description;
                        }
                        else
                        {
                            Debug.LogError($"Sprite not found at path: {imagePath}");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("No Image component found in the child of the button.");
                    }

                    i++;
                }
            }
        }
    }


    private readonly List<EAchievement> _plantAchievements = new()
    {
        EAchievement.NewGardener,
        EAchievement.BloomBooster,
        EAchievement.GardeningGuru,
        EAchievement.PerfectPlanter,
        EAchievement.PlantInvasion,
        EAchievement.BotanicalDiversity,
        EAchievement.MasterGardener
    };
        
    private readonly List<EAchievement> _levelUpAchievements = new()
    {
        EAchievement.QuickLearner,
        EAchievement.PlantPotential,
        EAchievement.LevelUpEnthusiast,
        EAchievement.LevelUpVeteran
    };
    
    private readonly List<EAchievement> _upgradeAchievements = new()
    {
        EAchievement.UpgradeApprentice,
        EAchievement.UpgradeMaster,
        EAchievement.UpgradeOverachiever
    };
    
    private readonly List<EAchievement> _fullyUpgradeAchievements = new()
    {
        EAchievement.FullyBloomed,
    };
    
    private readonly List<EAchievement> _dieAchievements = new()
    {
        EAchievement.FirstFall
    };
    
    private readonly List<EAchievement> _killAchievements = new()
    {
        EAchievement.FirstBlood,
        EAchievement.KillerSeed,
        EAchievement.EfficientKiller,
        EAchievement.MonsterFrenzy,
        EAchievement.MonsterSlayer,
        EAchievement.UnstoppableForce,
        EAchievement.EndlessOnslaught
    };

    private readonly List<EAchievement> _resourceAchievements = new()
    {
        EAchievement.ResourceCollector,
        EAchievement.ResourceHoarder,
        EAchievement.HerbalHarvester,
        EAchievement.ResourceTycoon,
        EAchievement.ResourcefulMind,
    };
    
    private readonly List<EAchievement> _sacrificeAchievements = new()
    {
        EAchievement.PlantSacrifice,
        EAchievement.AgainstAllOdds,
    };
    
    private readonly List<EAchievement> _unlockAchievements = new()
    {
        EAchievement.PlantCollector,
        EAchievement.UltimateGardener,
    };
    
    
    private int GetAchievementCounter(EAchievement achievement, int dataCount)
    {
        if (_plantAchievements.Contains(achievement))
        {
            var plantedPlantCount = PlayerManager.Instance.PlantedPlants;
            return Math.Min(dataCount, plantedPlantCount);
        }
        if (_levelUpAchievements.Contains(achievement))
        {
            var levelUpCount = PlayerManager.Instance.LevelUpCounter;
            return Math.Min(dataCount, levelUpCount);
        }
        if (_upgradeAchievements.Contains(achievement))
        {
            var upgradeCount = PlayerManager.Instance.UpgradePlantCounter;
            return Math.Min(dataCount, upgradeCount);
        }
        if (_fullyUpgradeAchievements.Contains(achievement))
        {
            var fullyUpgradeCount = PlayerManager.Instance.FullUpgradePlantCounter;
            return Math.Min(dataCount, fullyUpgradeCount);
        }
        if (_dieAchievements.Contains(achievement))
        {
            var dieCount = PlayerManager.Instance.Die;
            Debug.Log("Die Counts" + dieCount);
            return Math.Min(dataCount, dieCount);
        }
        if (_killAchievements.Contains(achievement))
        {
            var killCount = PlayerManager.Instance.Kill;
            return Math.Min(dataCount, killCount);
        }
        if (_resourceAchievements.Contains(achievement))
        {
            var resourceCount = PlayerManager.Instance.CollectResourceCounter;
            return Math.Min(dataCount, resourceCount);
        }
        if (_sacrificeAchievements.Contains(achievement))
        {
            var sacrificeCount = PlayerManager.Instance.SacrificeCounter;
            return Math.Min(dataCount, sacrificeCount);
        }
        if (_unlockAchievements.Contains(achievement))
        {
            var unlockCount = PlayerManager.Instance.UnlockedAchievements;
            return Math.Min(dataCount, unlockCount);
        }
        
        return 0;
    }
    
    private readonly List<EAchievement> _survivalAchievements = new()
    {
        EAchievement.SurvivalNotice,
        EAchievement.Survivalist,
        EAchievement.EnduranceExpert,
        EAchievement.BareMinimum,
        EAchievement.Untouchable,
        EAchievement.FlawlessDefense
    };
    
    private readonly List<EAchievement> _activePlantAchievements = new()
    {
        EAchievement.PlantCommander,
        EAchievement.PlantHoarder,
    };
    
    private readonly List<EAchievement> _explosiveAchievements = new()
    {
        EAchievement.ExplosiveExpertise,
    };
    
    private readonly List<EAchievement> _plantedInTimeAchievements = new()
    {
        EAchievement.SpeedPlanter,
        EAchievement.FrenziedFarmer,
    };
    
    private readonly List<EAchievement> _utilsAchievements = new()
    {
        EAchievement.TauntMaster,
        EAchievement.NaturesAvatar,
        EAchievement.GreenThumb,
        EAchievement.DoomsdayGardener,
        EAchievement.TrapSpecialist,
        EAchievement.ZenMaster,
        EAchievement.EcoWarrior
    };
    
    private bool GetAchievementCompleteness(EAchievement achievement)
    {

        if (_survivalAchievements.Contains(achievement))
        {
            var survivalData = PlayerManager.Instance.SurvivalData;
            var index = _survivalAchievements.IndexOf(achievement);
            return survivalData[index];
        }
        if (_activePlantAchievements.Contains(achievement))
        {
            var activePlantData = PlayerManager.Instance.ActivePlantData;
            var index = _activePlantAchievements.IndexOf(achievement);
            return activePlantData[index];
        }
        if (_explosiveAchievements.Contains(achievement))
        {
            var explodeCount = PlayerManager.Instance.ExplosiveData;
            var index = _explosiveAchievements.IndexOf(achievement);
            return explodeCount[index];
        }
        if (_plantedInTimeAchievements.Contains(achievement))
        {
            var plantedInTimeData = PlayerManager.Instance.PlantedInTimeData;
            var index = _plantedInTimeAchievements.IndexOf(achievement);
            return plantedInTimeData[index];
        }
        if (_utilsAchievements.Contains(achievement))
        {
            var utilsData = PlayerManager.Instance.UtilsData;
            var index = _utilsAchievements.IndexOf(achievement);
            return utilsData[index];
        }
        
        return false;
    }
}

    

