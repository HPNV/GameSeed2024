using System;
using System.Collections.Generic;
using System.Linq;
using Building;
using Script;
using Firebase.Extensions;
using Firebase.Firestore;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class HomeBase : Entity
{
    private FirebaseFirestore db;
    [SerializeField] Bar expBar;
    [SerializeField] Bar HpBar;
    [SerializeField] TextMeshPro waterText;
    [SerializeField] TextMeshPro sunText;
    [SerializeField] TextMeshPro levelText;
    [SerializeField] TextMeshPro scoreText;
    [SerializeField] TextMeshPro timeText;
    [SerializeField] TextMeshPro healthText;
    private int currentLevel = 1;
    private int currentExp = 0;
    private int expToNextLevel = 100;
    [SerializeField] private float startHealth;
    public int water = 0;
    public int sun = 0;
    public int score = 0;
    public float time = 0;

    void Start()
    {
        Init(startHealth, startHealth);
        UpdateUI();
    }

    void Update()
    {
        time += Time.deltaTime;
        UpdateTimeText();
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     Vector3 mousePosition = Input.mousePosition;
        //     mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //     mousePosition.z = -2;
        //     SingletonGame.Instance.ExperienceManager.SpawnBatch(5, mousePosition);
        // }

        LevelUp();
    }

    private void UpdateTimeText()
    {
        int hours = Mathf.FloorToInt(time / 3600);
        int minutes = Mathf.FloorToInt((time % 3600) / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        timeText.text = $"{hours:00}:{minutes:00}:{seconds:00}";
        SingletonGame.Instance.loseScreen.time = $"{hours:00}:{minutes:00}:{seconds:00}";

        var pm = PlayerManager.Instance;
        if(minutes == 1) pm.OnSurviveAchievement(EAchievement.BareMinimum);
        if(minutes == 10) pm.OnSurviveAchievement(EAchievement.SurvivalNotice);
        if(minutes == 30) pm.OnSurviveAchievement(EAchievement.Survivalist);
        if(hours == 1) pm.OnSurviveAchievement(EAchievement.EnduranceExpert);
        if((pm.LastHitTimeStamp - DateTime.Now).TotalHours >= 1) pm.OnSurviveAchievement(EAchievement.Untouchable);
        if((pm.LastHitTimeStamp - DateTime.Now).TotalMinutes >= 20) pm.OnSurviveAchievement(EAchievement.FlawlessDefense);
    }

    private void LevelUp() {
        if(currentExp > expToNextLevel){
            currentLevel +=1;
            currentExp -= expToNextLevel;
            expToNextLevel += 50;
            UpdateUI();
            PlayerManager.Instance.OnPlayerLevelUp();
            SingletonGame.Instance.SpawnPlant();
        }
    }

    public void GainExp(int exp) {
        currentExp += exp;
        UpdateUI();
    }

    public void GainScore(int score) {
        this.score += score;
        UpdateUI();
    }

    public void UpdateUI() {
        expBar.Exp = currentExp;
        expBar.setMaxValue(expToNextLevel);
        HpBar.Exp = Health;
        HpBar.setMaxValue(MaxHealth);
        waterText.text = water.ToString();
        sunText.text = sun.ToString();
        levelText.text = "Lvl " + currentLevel.ToString();
        scoreText.text = score.ToString();
        healthText.text = this.Health.ToString() + "/" + this.MaxHealth;
    }

    public void addSun(int sun) {
        this.sun += sun;
    }

    public void addWater(int water) {
        this.water += water;
    }

    protected override bool ValidateDamage()
    {
        return true;
    }

    protected override void OnDamage(float dmg)
    {
        StartCoroutine(Flash(Color.red));
        CameraController.Instance.ShakeCamera();
        UpdateUI();
        PlayerManager.Instance.LastHitTimeStamp = DateTime.Now;
    }

    protected override void OnHeal()
    {
    }

    protected override void OnDie()
    {
        HighScoreUpdate();
        SingletonGame.Instance.LoseGame();
    }

    private void HighScoreUpdate()
    {
        string hostname = System.Environment.MachineName;
        db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("users").Document(hostname);
        
        // get the user highest_score then compare it to the current score
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DocumentSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    Dictionary<string, object> data = snapshot.ToDictionary();
                    int highest_score = (int) data["highest_score"];
                    if (score > highest_score)
                    {
                        UpdateHighScore(docRef);
                    }
                }
            }
            else
            {
                // Debug.LogError("Failed to fetch data: " + task.Exception);
            }
        });
    }

    private void UpdateHighScore(DocumentReference docRef)
    {
        
        Dictionary<string, object> data = new Dictionary<string, object>
        {
            {"highest_score", score}
        };
        docRef.SetAsync(data).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                // Debug.Log("High Score Updated");
            }
            else
            {
                // Debug.LogError("Failed to update high score: " + task.Exception);
            }
        });
    }

    protected override void OnSpeedUp() { }

    protected override void OnSpeedUpClear() { }

    private void OnMouseEnter()
    {
        var sp = transform.gameObject.GetComponent<SpriteRenderer>();
        var color = Color.white;
        color.a = 0.75f;
        sp.color = color;
    }

    private void OnMouseExit()
    {
        var sp = transform.gameObject.GetComponent<SpriteRenderer>();
        var color = Color.white;
        color.a = 1f;
        sp.color = color;
    }
    
}
