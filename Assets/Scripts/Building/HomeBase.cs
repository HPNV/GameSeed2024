using System;
using System.Collections.Generic;
using System.Linq;
using Building;
using Script;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class HomeBase : Entity
{
    [SerializeField] Bar expBar;
    [SerializeField] Bar HpBar;
    [SerializeField] TextMeshPro waterText;
    [SerializeField] TextMeshPro sunText;
    [SerializeField] TextMeshPro levelText;
    [SerializeField] TextMeshPro scoreText;
    [SerializeField] TextMeshPro timeText;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            mousePosition.z = -2;
            SingletonGame.Instance.ExperienceManager.SpawnBatch(5, mousePosition);
        }

        LevelUp();
    }

    private void UpdateTimeText()
    {
        int hours = Mathf.FloorToInt(time / 3600);
        int minutes = Mathf.FloorToInt((time % 3600) / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        timeText.text = $"{hours:00}:{minutes:00}:{seconds:00}";
        SingletonGame.Instance.loseScreen.time = $"{hours:00}:{minutes:00}:{seconds:00}";
        
        if(minutes == 1) SingletonGame.Instance.PlayerManager.OnSurviveAchievement(EAchievement.BareMinimum);
        if(minutes == 10) SingletonGame.Instance.PlayerManager.OnSurviveAchievement(EAchievement.SurvivalNotice);
        if(minutes == 30) SingletonGame.Instance.PlayerManager.OnSurviveAchievement(EAchievement.Survivalist);
        if(hours == 1) SingletonGame.Instance.PlayerManager.OnSurviveAchievement(EAchievement.EnduranceExpert);
    }

    private void LevelUp() {
        if(currentExp > expToNextLevel){
            currentLevel +=1;
            currentExp -= expToNextLevel;
            expToNextLevel += 50;
            UpdateUI();
            SingletonGame.Instance.PlayerManager.OnPlayerLevelUp();
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

    protected override void OnDamage()
    {
        UpdateUI();
    }

    protected override void OnHeal()
    {
    }

    protected override void OnDie()
    {
        SingletonGame.Instance.LoseGame();
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
