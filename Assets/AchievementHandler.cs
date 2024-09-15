using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Firebase.Firestore;
using Firebase.Extensions;
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

        fetchUserData();
        
        ReconcileAchievement();
    }

    private void fetchUserData()
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
                PlayerManager.Instance.PlantedPlants = Convert.ToInt32(data["plant_counter"]);
                PlayerManager.Instance.EnemyExplodeCounter = Convert.ToInt32(data["explode_counter"]);
                PlayerManager.Instance.CollectResourceCounter = Convert.ToInt32(data["resource_counter"]);
                PlayerManager.Instance.UpgradePlantCounter = Convert.ToInt32(data["upgrade_counter"]);
                PlayerManager.Instance.LevelUpCounter = Convert.ToInt32(data["level_up_counter"]);
                // PlayerManager.Instance.HighestScore = Convert.ToInt32(data["highest_score"]);
                PlayerManager.Instance.FullUpgradePlantCounter = Convert.ToInt32(data["max_upgrade"]);
                PlayerManager.Instance.CompleteTutorial = Convert.ToBoolean(data["complete_tutorial"]);
            }
            else
            {
                Debug.Log("Document " + snapshot.Id + " does not exist!");
            }
        });
    }

    private static Dictionary<EAchievement, Dictionary<string, object>> _achievementData = new()
    {
        { EAchievement.NewGardener, new Dictionary<string, object>() { { "counter", 2 } } },
        { EAchievement.KillerSeed, new Dictionary<string, object>() { { "counter", 10 } } },
        { EAchievement.PlantPotential, new Dictionary<string, object>() { { "counter", 5 } } },
        { EAchievement.UpgradeApprentice, new Dictionary<string, object>() { { "counter", 3 } } },
        { EAchievement.FirstFall, new Dictionary<string, object>() { { "counter", 1 } } },
        { EAchievement.BloomBooster, new Dictionary<string, object>() { { "counter", 20 } } },
        { EAchievement.SurvivalNotice, new Dictionary<string, object>() { } },
        { EAchievement.Survivalist, new Dictionary<string, object>() { } },
        { EAchievement.LevelUpEnthusiast, new Dictionary<string, object>() { { "counter", 20 } } },
        { EAchievement.LevelUpVeteran, new Dictionary<string, object>() { { "counter", 50 } } },
        { EAchievement.GardeningGuru, new Dictionary<string, object>() { { "counter", 50 } } },
        { EAchievement.EnduranceExpert, new Dictionary<string, object>() { } },
        { EAchievement.UpgradeMaster, new Dictionary<string, object>() { { "counter", 10 } } },
        { EAchievement.UpgradeOverachiever, new Dictionary<string, object>() { { "counter", 30 } } },
        { EAchievement.ExplosiveExpertise, new Dictionary<string, object>() { { "counter", 5 } } },
        { EAchievement.ResourceCollector, new Dictionary<string, object>() { { "counter", 15 } } },
        { EAchievement.ResourceHoarder, new Dictionary<string, object>() { { "counter", 30 } } },
        { EAchievement.PlantSacrifice, new Dictionary<string, object>() { } },
        { EAchievement.BotanicalDiversity, new Dictionary<string, object>() { } },
        { EAchievement.HerbalHarvester, new Dictionary<string, object>() { { "counter", 50 } } },
        { EAchievement.MonsterSlayer, new Dictionary<string, object>() { { "counter", 500 } } },
        { EAchievement.PerfectPlanter, new Dictionary<string, object>() { { "counter", 100 } } },
        { EAchievement.FlawlessDefense, new Dictionary<string, object>() { } },
        { EAchievement.SpeedPlanter, new Dictionary<string, object>() { { "counter", 10 } } },
        { EAchievement.EfficientKiller, new Dictionary<string, object>() { { "counter", 50 } } },
        { EAchievement.TauntMaster, new Dictionary<string, object>() { } },
        { EAchievement.ResourceTycoon, new Dictionary<string, object>() { { "counter", 1000 } } },
        { EAchievement.PlantCollector, new Dictionary<string, object>() { } },
        { EAchievement.UnstoppableForce, new Dictionary<string, object>() { { "counter", 1000 } } },
        { EAchievement.GardenersGlory, new Dictionary<string, object>() {  } },
        { EAchievement.TrapSpecialist, new Dictionary<string, object>() { { "counter", 50 } } },
        { EAchievement.FirstBlood, new Dictionary<string, object>() { { "counter", 1 } } },
        { EAchievement.PlantInvasion, new Dictionary<string, object>() { { "counter", 1000 } } },
        { EAchievement.NaturesAvatar, new Dictionary<string, object>() { } },
        { EAchievement.GreenThumb, new Dictionary<string, object>() { } },
        { EAchievement.MonsterFrenzy, new Dictionary<string, object>() { { "counter", 200 } } },
        { EAchievement.PlantCommander, new Dictionary<string, object>() { { "counter", 50 } } },
        { EAchievement.AgainstAllOdds, new Dictionary<string, object>() { } },
        { EAchievement.DoomsdayGardener, new Dictionary<string, object>() { { "counter", 200 } } },
        { EAchievement.ZenMaster, new Dictionary<string, object>() { } },
        { EAchievement.EcoWarrior, new Dictionary<string, object>() { } },
        { EAchievement.BareMinimum, new Dictionary<string, object>() { } },
        { EAchievement.PlantHoarder, new Dictionary<string, object>() { { "counter", 100 } } },
        { EAchievement.MasterGardener, new Dictionary<string, object>() { { "counter", 5000 } } },
        { EAchievement.ResourcefulMind, new Dictionary<string, object>() { { "counter", 5000 } } },
        { EAchievement.EndlessOnslaught, new Dictionary<string, object>() { { "counter", 5000 } } },
        { EAchievement.FrenziedFarmer, new Dictionary<string, object>() { { "counter", 50 } } },
        { EAchievement.Untouchable, new Dictionary<string, object>() { } },
        { EAchievement.UltimateGardener, new Dictionary<string, object>() { } },
        { EAchievement.QuickLearner, new Dictionary<string, object>() { { "counter", 3 } } },
        { EAchievement.FullyBloomed, new Dictionary<string, object>() { { "counter", 1 } } },
    };
  

    private void ReconcileAchievement()
    {
        var data = PlayerManager.Instance.AchievementManager.Achievements;
        var keys = data.Keys.ToList();
        int i = 0;
        var selectedKey = keys[i];
        
        // Loop through each child (row) of the seedpediaPanel
        foreach (Transform child in seedpediaPanel.transform)
        {

            // Iterate through buttons in each row (grandchildren of seedpediaPanel)
            foreach (Transform grandchild in child)
            {

                Button button = grandchild.GetComponent<Button>();
                if (button != null)
                {
                    // Set the background sprite for the button
                    button.image.sprite = backgroundSprite;

                    // Get all Image components in the button's children (ignoring the button's own image)
                    Image[] images = button.GetComponentsInChildren<Image>();
                    TextMeshProUGUI[] texts = button.GetComponentsInChildren<TextMeshProUGUI>();
                    Slider[] sliders = button.GetComponentsInChildren<Slider>();
                    //
                    // // Ensure the second image exists (assuming the plant image is in the second position)
                    if (images.Length > 1)
                    {
                        
                        string plantId = data[selectedKey].name;
                    
                        // Construct the image path for the plant sprite
                        string imagePath = "Images/Achievements/" + plantId.Replace(' ', '_').ToLower();
                        
                        string formattedName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(data[selectedKey].name.Replace('_', ' '));
                    
                        // Load the sprite from the Resources folder
                        Sprite plantSprite = Resources.Load<Sprite>(imagePath);
                    
                        if (plantSprite != null)
                        {
                            // Set the plant image sprite
                            images[1].sprite = plantSprite;
                            texts[0].text = formattedName;
                            if(_achievementData[selectedKey].ContainsKey("counter"))
                            {
                                int achievementCounter = 0;
                                int counter = Convert.ToInt32(_achievementData[selectedKey]["counter"]);
                                
                                if (data[selectedKey].name is "new_gardener" || data[selectedKey].name is "bloom_booster" || data[selectedKey].name is "gardening_guru" || data[selectedKey].name is "perfect_planter" || data[selectedKey].name is "plant_invasion" || data[selectedKey].name is "master_gardener")
                                {
                                    achievementCounter = (PlayerManager.Instance.PlantedPlants > counter) ? counter : PlayerManager.Instance.PlantedPlants;
                                } 
                                else if(data[selectedKey].name is "quick_learner" || data[selectedKey].name is "plant_potential" || data[selectedKey].name is "level_up_enthusiast" || data[selectedKey].name is "level_up_veteran")
                                {
                                    achievementCounter = (PlayerManager.Instance.LevelUpCounter > counter) ? counter : PlayerManager.Instance.LevelUpCounter;
                                } 
                                else if(data[selectedKey].name is "upgrade_apprentice" || data[selectedKey].name is "upgrade_master" || data[selectedKey].name is "upgrade_overarchieve")
                                {
                                    achievementCounter = (PlayerManager.Instance.UpgradePlantCounter > counter) ? counter : PlayerManager.Instance.UpgradePlantCounter;
                                } 
                                else if(data[selectedKey].name is "fully_bloomed" || data[selectedKey].name is "gardening_glory")
                                {
                                    achievementCounter = (PlayerManager.Instance.FullUpgradePlantCounter > counter) ? counter : PlayerManager.Instance.FullUpgradePlantCounter;
                                } 
                                else if(data[selectedKey].name.ToString() == "first_fall")
                                {
                                  
                                    achievementCounter = (PlayerManager.Instance.Die > counter) ? counter : PlayerManager.Instance.Die;
                                    Debug.Log("FIRST FALLL" + achievementCounter);
                                } 
                                else if(data[selectedKey].name is "first_blood" || data[selectedKey].name is "killer_seed" || data[selectedKey].name is "efficient_killer" || data[selectedKey].name is "monster_frenzy" || data[selectedKey].name is "monster_slayer" || data[selectedKey].name is "unstoppable_force" || data[selectedKey].name is "endless_onslaught")
                                {
                                    achievementCounter = (PlayerManager.Instance.Kill > counter) ? counter : PlayerManager.Instance.Kill;
                                }
                                
                                texts[1].text = achievementCounter + "/" + _achievementData[selectedKey]["counter"];
                            }
                            
                            texts[2].text = data[selectedKey].description;
                        }
                        else
                        {
                            Debug.LogError("Sprite not found at path: " + imagePath);
                        }
                    
                        // Debug log for the loaded ID
                        Debug.Log("Loaded Plant ID: " + plantId);
                    }
                    else
                    {
                        Debug.LogWarning("No Image component found in the child of the button.");
                    }
                    //
                    // // Clear previous listeners to avoid multiple events triggering
                    // button.onClick.RemoveAllListeners();
                    //
                    // // Use a local copy of i for the lambda closure
                    int index = i;
                    // Debug.Log(achievementList[index]);
                    // // button.onClick.AddListener(() => OnButtonClick(seedpediaList[index]));
                    //
                    selectedKey = keys[++i];
                }
            }
        }
    }

    
}
