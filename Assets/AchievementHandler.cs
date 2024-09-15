using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Firebase.Firestore;
using Firebase.Extensions;
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
                SingletonGame.Instance.PlayerManager.Die = Convert.ToInt32(data["die_counter"]);
                SingletonGame.Instance.PlayerManager.Kill = Convert.ToInt32(data["kill_counter"]);
                SingletonGame.Instance.PlayerManager.PlantedPlants = Convert.ToInt32(data["plant_counter"]);
                SingletonGame.Instance.PlayerManager.EnemyExplodeCounter = Convert.ToInt32(data["explode_counter"]);
                SingletonGame.Instance.PlayerManager.CollectResourceCounter = Convert.ToInt32(data["resource_counter"]);
                SingletonGame.Instance.PlayerManager.UpgradePlantCounter = Convert.ToInt32(data["upgrade_counter"]);
                SingletonGame.Instance.PlayerManager.LevelUpCounter = Convert.ToInt32(data["level_up_counter"]);
                // SingletonGame.Instance.PlayerManager.HighestScore = Convert.ToInt32(data["highest_score"]);
                SingletonGame.Instance.PlayerManager.FullUpgradePlantCounter = Convert.ToInt32(data["max_upgrade"]);
                SingletonGame.Instance.PlayerManager.CompleteTutorial = Convert.ToBoolean(data["complete_tutorial"]);
            }
            else
            {
                Debug.Log("Document " + snapshot.Id + " does not exist!");
            }
        });
    }

    private static List<Dictionary<string, object>> achievementList = new List<Dictionary<string, object>>
    {
        new() { { "name", "against_all_odds" }, { "description", "Complete the game with minimal resources." } },
        new() { { "name", "bare_minimum" }, { "description", "Achieve victory with the least possible effort." } },
        new() { { "name", "bloom_booster" }, { "description", "Boost the growth of plants significantly." }, {"counter", 20} },
        new() { { "name", "Botanical_Diversity_" }, { "description", "Cultivate a wide variety of plants." } },
        new() { { "name", "complete_tutorial" }, { "description", "Finish the tutorial." } },
        new() { { "name", "doomsday_garden" }, { "description", "Create a garden capable of surviving an apocalypse." }, {"counter", 200} },
        new() { { "name", "eco_warrior" }, { "description", "Defend the environment with plant power." } },
        new() { { "name", "efficient_killer" }, { "description", "Eliminate enemies with minimal effort." }, {"counter", 50} },
        new() { { "name", "endless_onslaught" }, { "description", "Survive against waves of enemies without end." }, {"counter", 5000} },
        new() { { "name", "endurance_expert" }, { "description", "Demonstrate exceptional stamina and resilience." } },
        new() { { "name", "explosive_expertise" }, { "description", "Master the use of explosives in battle."} , {"counter", 5} },
        new() { { "name", "first_blood" }, { "description", "Be the first to deal damage in a battle." }, {"counter", 1} },
        new() { { "name", "first_fall" }, { "description", "Experience your first defeat." }, {"die", 1} },
        new() { { "name", "flawless_defense" }, { "description", "Defend an area without taking any damaRge." } },
        new() { { "name", "frenzied_farmer" }, { "description", "Harvest an enormous number of plants in a short time." }, {"counter", 50} },
        new() { { "name", "fully bloomed" }, { "description", "Grow all plants to their full potential." }, {"counter", 1} },
        new() { { "name", "gardening_guru" }, { "description", "Reach mastery in gardening skills." }, {"counter", 50} },
        new() { { "name", "green_thumb" }, { "description", "Prove your natural talent for gardening." } },
        new() { { "name", "herbal_harvester" }, { "description", "Harvest a large variety of herbs." }, {"counter", 50} },
        new() { { "name", "killer_seed" }, { "description", "Use a seed to defeat a powerful enemy." }, {"counter", 10} },
        new() { { "name", "level_up_enthusiast" }, { "description", "Level up quickly and consistently." }, {"counter", 20} },
        new() { { "name", "level_up_veteran" }, { "description", "Reach high levels through experience and dedication." }, {"counter", 50} },
        new() { { "name", "master_gardener" }, { "description", "Attain the title of master gardener." }, {"counter", 5000} },
        new() { { "name", "monster_frenzy" }, { "description", "Defeat monsters in rapid succession." }, {"counter", 200} },
        new() { { "name", "monster_slayer" }, { "description", "Slay a large number of monsters." }, {"counter", 500} },
        new() { { "name", "natures_avatar" }, { "description", "Become one with nature and its creatures." } },
        new() { { "name", "new_gardener" }, { "description", "Begin your journey as a gardener." }, {"counter", 2} },
        new() { { "name", "perfect_planter" }, { "description", "Plant seeds with precision and care." }, {"counter", 100} },
        new() { { "name", "plant_collector" }, { "description", "Collect a wide array of different plant species." } },
        new() { { "name", "plant_commander" }, { "description", "Command a group of plants to fight for you." }, {"counter", 50} },
        new() { { "name", "plant_hoarder" }, { "description", "Accumulate a vast number of plants." }, {"counter", 100} },
        new() { { "name", "plant_invasion" }, { "description", "Unleash a plant invasion on your enemies." }, {"counter", 1000} },
        new() { { "name", "plant_potential" }, { "description", "Realize the full potential of your plants." }, {"counter", 5} },
        new() { { "name", "plant_sacrificer" }, { "description", "Sacrifice plants to achieve victory." } },
        new() { { "name", "quick_learner" }, { "description", "Quickly master new skills and concepts." }, {"counter", 3} },
        new() { { "name", "resourceful_mind" }, { "description", "Make the most of your available resources." }, {"counter", 5000} },
        new() { { "name", "resource_collector" }, { "description", "Gather a wide variety of resources." }, {"counter", 15} },
        new() { { "name", "resource_hoarder" }, { "description", "Amass an enormous amount of resources." }, {"counter", 30} },
        new() { { "name", "resource_tycoon" }, { "description", "Become a master of resource management." }, {"counter", 1000} },
        new() { { "name", "speed_planter" }, { "description", "Plant seeds at an incredible pace." }, {"counter", 10} },
        new() { { "name", "survivalist" }, { "description", "Survive against all odds." } },
        new() { { "name", "survival_notice" }, { "description", "Take action in time to ensure survival." } },
        new() { { "name", "taunt_master" }, { "description", "Master the art of taunting enemies." } },
        new() { { "name", "trap_specialist" }, { "description", "Become an expert at setting traps." }, {"counter", 50} },
        new() { { "name", "ultimate_gardener" }, { "description", "Achieve the highest level of gardening." } },
        new() { { "name", "unstoppable_force" }, { "description", "Become an unstoppable force in battle." }, {"counter", 1000} },
        new() { { "name", "untouchable" }, { "description", "Avoid all damage during a fight." } },
        new() { { "name", "upgrade_apprentice" }, { "description", "Learn the basics of upgrading items." }, {"counter", 3} },
        new() { { "name", "upgrade_master" }, { "description", "Master the art of upgrading." }, {"counter", 10} },
        new() { { "name", "upgrade_overarchieve" }, { "description", "Go beyond expectations in upgrading items." }, {"counter", 30} },
        new() { { "name", "zen_master" }, { "description", "Achieve ultimate peace and balance." } }
    };

    private void ReconcileAchievement() 
    {
        int i = 0;
        
        // Loop through each child (row) of the seedpediaPanel
        foreach (Transform child in seedpediaPanel.transform)
        {
            if (i >= achievementList.Count)
            {
                Debug.LogWarning("More buttons than available data");
                break;
            }

            // Iterate through buttons in each row (grandchildren of seedpediaPanel)
            foreach (Transform grandchild in child)
            {
                if (i >= achievementList.Count)
                {
                    Debug.LogWarning("More buttons than available data");
                    break;
                }

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
                        string plantId = achievementList[i]["name"].ToString();
                    
                        // Construct the image path for the plant sprite
                        string imagePath = "Images/Achievements/" + plantId;
                        
                        string formattedName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(achievementList[i]["name"].ToString().Replace('_', ' '));
                    
                        // Load the sprite from the Resources folder
                        Sprite plantSprite = Resources.Load<Sprite>(imagePath);
                    
                        if (plantSprite != null)
                        {
                            // Set the plant image sprite
                            images[1].sprite = plantSprite;
                            texts[0].text = formattedName;
                            if(achievementList[i].ContainsKey("counter"))
                            {
                                int achievementCounter = 0;
                                int counter = Convert.ToInt32(achievementList[i]["counter"]);

                                if (achievementList[i]["name"] is "new_gardener" || achievementList[i]["name"] is "bloom_booster" || achievementList[i]["name"] is "gardening_guru" || achievementList[i]["name"] is "perfect_planter" || achievementList[i]["name"] is "plant_invasion" || achievementList[i]["name"] is "master_gardener")
                                {
                                    achievementCounter = (SingletonGame.Instance.PlayerManager.PlantedPlants > counter) ? counter : SingletonGame.Instance.PlayerManager.PlantedPlants;
                                } else if(achievementList[i]["name"] is "quick_learner" || achievementList[i]["name"] is "plant_potential" || achievementList[i]["name"] is "level_up_enthusiast" || achievementList[i]["name"] is "level_up_veteran")
                                {
                                    achievementCounter = (SingletonGame.Instance.PlayerManager.LevelUpCounter > counter) ? counter : SingletonGame.Instance.PlayerManager.LevelUpCounter;
                                } else if(achievementList[i]["name"] is "upgrade_apprentice" || achievementList[i]["name"] is "upgrade_master" || achievementList[i]["name"] is "upgrade_overarchieve")
                                {
                                    achievementCounter = (SingletonGame.Instance.PlayerManager.UpgradePlantCounter > counter) ? counter : SingletonGame.Instance.PlayerManager.UpgradePlantCounter;
                                } else if(achievementList[i]["name"] is "fully_bloomed" || achievementList[i]["name"] is "gardening_glory")
                                {
                                    achievementCounter = (SingletonGame.Instance.PlayerManager.FullUpgradePlantCounter > counter) ? counter : SingletonGame.Instance.PlayerManager.FullUpgradePlantCounter;
                                } else if(achievementList[i]["name"] is "first_fall")
                                {
                                    achievementCounter = (SingletonGame.Instance.PlayerManager.Die > counter) ? counter : SingletonGame.Instance.PlayerManager.Die;
                                } else if(achievementList[i]["name"] is "first_blood" || achievementList[i]["name"] is "killer_seed" || achievementList[i]["name"] is "efficient_killer" || achievementList[i]["name"] is "monster_frenzy" || achievementList[i]["name"] is "monster_slayer" || achievementList[i]["name"] is "unstoppable_force" || achievementList[i]["name"] is "endless_onslaught")
                                {
                                    achievementCounter = (SingletonGame.Instance.PlayerManager.Kill > counter) ? counter : SingletonGame.Instance.PlayerManager.Kill;
                                }
                                
                                texts[1].text = achievementCounter + "/" + achievementList[i]["counter"];
                            }
                            
                            texts[2].text = achievementList[i]["description"].ToString();
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
                    i++; // Move to the next plant in the list
                }
            }
        }
    }

    
}
