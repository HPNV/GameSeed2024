using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Firestore;
using Firebase.Extensions;
using Plant;
using TMPro;

public class SeedpediaHandler : MonoBehaviour
{
    private FirebaseFirestore db;
    [SerializeField] private GameObject seedpediaPanel;
    [SerializeField] private Sprite backgroundSprite;

    [SerializeField] private GameObject seedDetail;
    [SerializeField] private GameObject plantImage;
    [SerializeField] private TextMeshProUGUI plantName;
    [SerializeField] private TextMeshProUGUI plantDescription;
    [SerializeField] private TextMeshProUGUI healthValue;
    [SerializeField] private TextMeshProUGUI atkValue;
    [SerializeField] private TextMeshProUGUI atkSpeedValue;
    [SerializeField] private TextMeshProUGUI growTimeValue;

    private List<Dictionary<string, object>> seedpediaList = new List<Dictionary<string, object>>();
    
    [SerializeField] private string seedpediaCollection = "seedpedia";

    void Start()
    {
        // Initialize Firestore
        db = FirebaseFirestore.DefaultInstance;
        
        // Fetch data from Firestore
        GetSeedpediaData();
    }

    void GetSeedpediaData()
    {
        CollectionReference seedpediaRef = db.Collection(seedpediaCollection);

        // Fetch all documents in the "seedpedia" collection
        seedpediaRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                QuerySnapshot snapshot = task.Result;

                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    // Extract the fields from each document
                    Dictionary<string, object> data = document.ToDictionary();

                    // Add the document ID to the dictionary
                    data.Add("id", document.Id);
                    
                    seedpediaList.Add(data);
                }

                // After fetching all data, update the UI
                if(seedpediaCollection == "seedpedia")
                {
                    ReconcileSeedpedia();    
                }
                else
                {
                    ReconcileSlimePedia();
                }    
            }
            else
            {
                Debug.LogError("Failed to retrieve data from Firestore: " + task.Exception);
            }
        });
    }
    
    private void ReconcileSlimePedia() 
    {
        int i = 0;
        
        // Loop through each child (row) of the seedpediaPanel
        foreach (Transform child in seedpediaPanel.transform)
        {
            if (i >= seedpediaList.Count)
            {
                Debug.LogWarning("More buttons than available data");
                break;
            }

            // Iterate through buttons in each row (grandchildren of seedpediaPanel)
            foreach (Transform grandchild in child)
            {
                if (i >= seedpediaList.Count)
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

                    // Ensure the second image exists (assuming the plant image is in the second position)
                    if (images.Length > 1)
                    {
                        string plantId = seedpediaList[i]["id"].ToString();

                        // Construct the image path for the plant sprite
                        string imagePath = "Images/Enemy/" + plantId;

                        // Load the sprite from the Resources folde
                        Sprite plantSprite = Resources.Load<Sprite>(imagePath);

                        if (plantSprite != null)
                        {
                            // Set the plant image sprite
                            images[1].sprite = plantSprite;
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

                    // Clear previous listeners to avoid multiple events triggering
                    button.onClick.RemoveAllListeners();

                    // Use a local copy of i for the lambda closure
                    int index = i;
                    button.onClick.AddListener(() => OnButtonClickSlime(seedpediaList[index]));

                    i++; // Move to the next plant in the list
                }
            }
        }
    }
    
    private void ReconcileSeedpedia()
    {
        int i = 0;

        // Loop through each child (row) of the seedpediaPanel
        foreach (Transform child in seedpediaPanel.transform)
        {
            if (i >= seedpediaList.Count)
            {
                Debug.LogWarning("More buttons than available data");
                break;
            }

            // Iterate through buttons in each row (grandchildren of seedpediaPanel)
            foreach (Transform grandchild in child)
            {
                if (i >= seedpediaList.Count)
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

                    // Ensure the second image exists (assuming the plant image is in the second position)
                    if (images.Length > 1)
                    {
                        string plantId = seedpediaList[i]["id"].ToString();

                        // Construct the image path for the plant sprite
                        string imagePath = "Images/Plant/" + plantId + "/Idle/idle_2";

                        // Load the sprite from the Resources folder
                        Sprite plantSprite = Resources.Load<Sprite>(imagePath);

                        if (plantSprite != null)
                        {
                            // Set the plant image sprite
                            images[1].sprite = plantSprite;
                        }
                        else
                        {
                            Debug.LogError("Sprite not found at path: " + imagePath);
                        }

                        // Debug log for the loaded ID
                        // Debug.Log("Loaded Plant ID: " + plantId);
                    }
                    else
                    {
                        Debug.LogWarning("No Image component found in the child of the button.");
                    }

                    // Clear previous listeners to avoid multiple events triggering
                    button.onClick.RemoveAllListeners();

                    // Use a local copy of i for the lambda closure
                    int index = i;
                    button.onClick.AddListener(() => OnButtonClick(seedpediaList[index]));

                    i++; // Move to the next plant in the list
                }
            }
        }
    }

    private void OnButtonClick(Dictionary<string, object> data)
    {
        string plantId = data["id"].ToString();
        string plantDescriptionText = data["description"].ToString();
        string healthValue = data["health"].ToString();
        string atkValue = data["atk"].ToString();
        string atkSpeedValue = data["atk_speed"].ToString();
        string growTimeValue = data["grow_time"].ToString();
    
        string imagePath = "Images/Plant/" + plantId + "/Idle/idle_2";
    
        Sprite plantSprite = Resources.Load<Sprite>(imagePath);
        
        Debug.Log("Image Path: " + plantSprite);

        var plantSpriteRenderer = plantImage.GetComponent<SpriteRenderer>();
        var plantAnimator = plantImage.GetComponent<Animator>();
    
        if (plantSprite != null)
        {
            plantSpriteRenderer.sprite = plantSprite;
            
            plantSpriteRenderer.transform.localScale = new Vector3(320f, 320f, 4f);
            plantSpriteRenderer.transform.localPosition = new Vector3(0f, 121.999f, -201f);
            
            PlantData a = Resources.Load<PlantData>("Plant/" + plantId);

            plantAnimator.runtimeAnimatorController = a.animatorController;
            plantAnimator.Play("Idle");
        }
        else
        {
            Debug.LogError("Sprite not found at path: " + imagePath);
        }
        
        this.plantName.text = plantId;
        this.plantDescription.text = plantDescriptionText;
        this.healthValue.text = healthValue;
        this.atkValue.text = atkValue;
        this.atkSpeedValue.text = atkSpeedValue;
        this.growTimeValue.text = growTimeValue;
    }
    
    private void OnButtonClickSlime(Dictionary<string, object> data)
    {
        string plantId = data["id"].ToString();
        string plantDescriptionText = data["description"].ToString();
    
        string imagePath = "Images/Enemy/" + plantId + "/Walk/walk_2";
    
        Sprite plantSprite = Resources.Load<Sprite>(imagePath);
        
        Debug.Log("Image Path: " + plantSprite);

        var plantSpriteRenderer = plantImage.GetComponent<SpriteRenderer>();
        var plantAnimator = plantImage.GetComponent<Animator>();
    
        if (plantSprite != null)
        {
            plantSpriteRenderer.sprite = plantSprite;
            
            plantSpriteRenderer.transform.localScale = new Vector3(320f, 320f, 4f);
            plantSpriteRenderer.transform.localPosition = new Vector3(0f, 121.999f, -201f);
            
            EnemyData a = Resources.Load<EnemyData>("Enemy/" + plantId);
            Debug.Log("Enemy/" + plantId);
            plantAnimator.runtimeAnimatorController = a.animatorController;
            plantAnimator.Play("Walk");
        }
        else
        {
            Debug.LogError("Sprite not found at path: " + imagePath);
        }
        
        this.plantName.text = plantId;
        this.plantDescription.text = plantDescriptionText;
    }
}
