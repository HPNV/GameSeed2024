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
    
    private static List<Dictionary<string, object>> achievementList = new List<Dictionary<string, object>>
    {
        new() { { "name", "against_all_odds" }, { "description", "Complete the game with minimal resources." } },
        new() { { "name", "bare_minimum" }, { "description", "Achieve victory with the least possible effort." } },
        new() { { "name", "bloom_booster" }, { "description", "Boost the growth of plants significantly." } },
        new() { { "name", "Botanical_Diversity_" }, { "description", "Cultivate a wide variety of plants." } },
        new() { { "name", "complete_tutorial" }, { "description", "Finish the tutorial." } },
        new() { { "name", "doomsday_garden" }, { "description", "Create a garden capable of surviving an apocalypse." } },
        new() { { "name", "eco_warrior" }, { "description", "Defend the environment with plant power." } },
        new() { { "name", "efficient_killer" }, { "description", "Eliminate enemies with minimal effort." } },
        new() { { "name", "endless_onslaught" }, { "description", "Survive against waves of enemies without end." } },
        new() { { "name", "endurance_expert" }, { "description", "Demonstrate exceptional stamina and resilience." } },
        new() { { "name", "explosive_expertise" }, { "description", "Master the use of explosives in battle." } },
        new() { { "name", "first_blood" }, { "description", "Be the first to deal damage in a battle." } },
        new() { { "name", "first_fall" }, { "description", "Experience your first defeat." } },
        new() { { "name", "flawless_defense" }, { "description", "Defend an area without taking any damage." } },
        new() { { "name", "frenzied_farmer" }, { "description", "Harvest an enormous number of plants in a short time." } },
        new() { { "name", "fully bloomed" }, { "description", "Grow all plants to their full potential." } },
        new() { { "name", "gardening_guru" }, { "description", "Reach mastery in gardening skills." } },
        new() { { "name", "green_thumb" }, { "description", "Prove your natural talent for gardening." } },
        new() { { "name", "herbal_harvester" }, { "description", "Harvest a large variety of herbs." } },
        new() { { "name", "killer_seed" }, { "description", "Use a seed to defeat a powerful enemy." } },
        new() { { "name", "level_up_enthusiast" }, { "description", "Level up quickly and consistently." } },
        new() { { "name", "level_up_veteran" }, { "description", "Reach high levels through experience and dedication." } },
        new() { { "name", "master_gardener" }, { "description", "Attain the title of master gardener." } },
        new() { { "name", "monster_frenzy" }, { "description", "Defeat monsters in rapid succession." } },
        new() { { "name", "monster_slayer" }, { "description", "Slay a large number of monsters." } },
        new() { { "name", "natures_avatar" }, { "description", "Become one with nature and its creatures." } },
        new() { { "name", "new_gardener" }, { "description", "Begin your journey as a gardener." } },
        new() { { "name", "perfect_planter" }, { "description", "Plant seeds with precision and care." } },
        new() { { "name", "plant_collector" }, { "description", "Collect a wide array of different plant species." } },
        new() { { "name", "plant_commander" }, { "description", "Command a group of plants to fight for you." } },
        new() { { "name", "plant_hoarder" }, { "description", "Accumulate a vast number of plants." } },
        new() { { "name", "plant_invasion" }, { "description", "Unleash a plant invasion on your enemies." } },
        new() { { "name", "plant_potential" }, { "description", "Realize the full potential of your plants." } },
        new() { { "name", "plant_sacrificer" }, { "description", "Sacrifice plants to achieve victory." } },
        new() { { "name", "quick_learner" }, { "description", "Quickly master new skills and concepts." } },
        new() { { "name", "resourceful_mind" }, { "description", "Make the most of your available resources." } },
        new() { { "name", "resource_collector" }, { "description", "Gather a wide variety of resources." } },
        new() { { "name", "resource_hoarder" }, { "description", "Amass an enormous amount of resources." } },
        new() { { "name", "resource_tycoon" }, { "description", "Become a master of resource management." } },
        new() { { "name", "speed_planter" }, { "description", "Plant seeds at an incredible pace." } },
        new() { { "name", "survivalist" }, { "description", "Survive against all odds." } },
        new() { { "name", "survival_notice" }, { "description", "Take action in time to ensure survival." } },
        new() { { "name", "taunt_master" }, { "description", "Master the art of taunting enemies." } },
        new() { { "name", "trap_specialist" }, { "description", "Become an expert at setting traps." } },
        new() { { "name", "ultimate_gardener" }, { "description", "Achieve the highest level of gardening." } },
        new() { { "name", "unstoppable_force" }, { "description", "Become an unstoppable force in battle." } },
        new() { { "name", "untouchable" }, { "description", "Avoid all damage during a fight." } },
        new() { { "name", "upgrade_apprentice" }, { "description", "Learn the basics of upgrading items." } },
        new() { { "name", "upgrade_master" }, { "description", "Master the art of upgrading." } },
        new() { { "name", "upgrade_overarchieve" }, { "description", "Go beyond expectations in upgrading items." } },
        new() { { "name", "zen_master" }, { "description", "Achieve ultimate peace and balance." } }
    };
    
    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        
        ReconcileAchievement();
    }

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
                            // 
                            
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
