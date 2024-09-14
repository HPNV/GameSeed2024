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
        new Dictionary<string, object> { { "name", "against_all_odds" }, { "description", "Complete the game with minimal resources." } },
        new Dictionary<string, object> { { "name", "bare_minimum" }, { "description", "Achieve victory with the least possible effort." } },
        new Dictionary<string, object> { { "name", "bloom_booster" }, { "description", "Boost the growth of plants significantly." } },
        new Dictionary<string, object> { { "name", "Botanical_Diversity_" }, { "description", "Cultivate a wide variety of plants." } },
        new Dictionary<string, object> { { "name", "complete_tutorial" }, { "description", "Finish the tutorial." } },
        new Dictionary<string, object> { { "name", "doomsday_garden" }, { "description", "Create a garden capable of surviving an apocalypse." } },
        new Dictionary<string, object> { { "name", "eco_warrior" }, { "description", "Defend the environment with plant power." } },
        new Dictionary<string, object> { { "name", "efficient_killer" }, { "description", "Eliminate enemies with minimal effort." } },
        new Dictionary<string, object> { { "name", "endless_onslaught" }, { "description", "Survive against waves of enemies without end." } },
        new Dictionary<string, object> { { "name", "endurance_expert" }, { "description", "Demonstrate exceptional stamina and resilience." } },
        new Dictionary<string, object> { { "name", "explosive_expertise" }, { "description", "Master the use of explosives in battle." } },
        new Dictionary<string, object> { { "name", "first_blood" }, { "description", "Be the first to deal damage in a battle." } },
        new Dictionary<string, object> { { "name", "first_fall" }, { "description", "Experience your first defeat." } },
        new Dictionary<string, object> { { "name", "flawless_defense" }, { "description", "Defend an area without taking any damage." } },
        new Dictionary<string, object> { { "name", "frenzied_farmer" }, { "description", "Harvest an enormous number of plants in a short time." } },
        new Dictionary<string, object> { { "name", "fully bloomed" }, { "description", "Grow all plants to their full potential." } },
        new Dictionary<string, object> { { "name", "gardening_guru" }, { "description", "Reach mastery in gardening skills." } },
        new Dictionary<string, object> { { "name", "green_thumb" }, { "description", "Prove your natural talent for gardening." } },
        new Dictionary<string, object> { { "name", "herbal_harvester" }, { "description", "Harvest a large variety of herbs." } },
        new Dictionary<string, object> { { "name", "killer_seed" }, { "description", "Use a seed to defeat a powerful enemy." } },
        new Dictionary<string, object> { { "name", "level_up_enthusiast" }, { "description", "Level up quickly and consistently." } },
        new Dictionary<string, object> { { "name", "level_up_veteran" }, { "description", "Reach high levels through experience and dedication." } },
        new Dictionary<string, object> { { "name", "master_gardener" }, { "description", "Attain the title of master gardener." } },
        new Dictionary<string, object> { { "name", "monster_frenzy" }, { "description", "Defeat monsters in rapid succession." } },
        new Dictionary<string, object> { { "name", "monster_slayer" }, { "description", "Slay a large number of monsters." } },
        new Dictionary<string, object> { { "name", "natures_avatar" }, { "description", "Become one with nature and its creatures." } },
        new Dictionary<string, object> { { "name", "new_gardener" }, { "description", "Begin your journey as a gardener." } },
        new Dictionary<string, object> { { "name", "perfect_planter" }, { "description", "Plant seeds with precision and care." } },
        new Dictionary<string, object> { { "name", "plant_collector" }, { "description", "Collect a wide array of different plant species." } },
        new Dictionary<string, object> { { "name", "plant_commander" }, { "description", "Command a group of plants to fight for you." } },
        new Dictionary<string, object> { { "name", "plant_hoarder" }, { "description", "Accumulate a vast number of plants." } },
        new Dictionary<string, object> { { "name", "plant_invasion" }, { "description", "Unleash a plant invasion on your enemies." } },
        new Dictionary<string, object> { { "name", "plant_potential" }, { "description", "Realize the full potential of your plants." } },
        new Dictionary<string, object> { { "name", "plant_sacrificer" }, { "description", "Sacrifice plants to achieve victory." } },
        new Dictionary<string, object> { { "name", "quick_learner" }, { "description", "Quickly master new skills and concepts." } },
        new Dictionary<string, object> { { "name", "resourceful_mind" }, { "description", "Make the most of your available resources." } },
        new Dictionary<string, object> { { "name", "resource_collector" }, { "description", "Gather a wide variety of resources." } },
        new Dictionary<string, object> { { "name", "resource_hoarder" }, { "description", "Amass an enormous amount of resources." } },
        new Dictionary<string, object> { { "name", "resource_tycoon" }, { "description", "Become a master of resource management." } },
        new Dictionary<string, object> { { "name", "speed_planter" }, { "description", "Plant seeds at an incredible pace." } },
        new Dictionary<string, object> { { "name", "survivalist" }, { "description", "Survive against all odds." } },
        new Dictionary<string, object> { { "name", "survival_notice" }, { "description", "Take action in time to ensure survival." } },
        new Dictionary<string, object> { { "name", "taunt_master" }, { "description", "Master the art of taunting enemies." } },
        new Dictionary<string, object> { { "name", "trap_specialist" }, { "description", "Become an expert at setting traps." } },
        new Dictionary<string, object> { { "name", "ultimate_gardener" }, { "description", "Achieve the highest level of gardening." } },
        new Dictionary<string, object> { { "name", "unstoppable_force" }, { "description", "Become an unstoppable force in battle." } },
        new Dictionary<string, object> { { "name", "untouchable" }, { "description", "Avoid all damage during a fight." } },
        new Dictionary<string, object> { { "name", "upgrade_apprentice" }, { "description", "Learn the basics of upgrading items." } },
        new Dictionary<string, object> { { "name", "upgrade_master" }, { "description", "Master the art of upgrading." } },
        new Dictionary<string, object> { { "name", "upgrade_overarchieve" }, { "description", "Go beyond expectations in upgrading items." } },
        new Dictionary<string, object> { { "name", "zen_master" }, { "description", "Achieve ultimate peace and balance." } }
    };
    
    // Start is called before the first frame update
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
