using System.Collections.Generic;
using System.Linq;
using Building;
using TMPro;
using UnityEngine;
using Utils;

public class HomeBase : Entity
{
    [SerializeField] Bar expBar;
    [SerializeField] Bar HpBar;
    [SerializeField] TextMeshPro waterText;
    [SerializeField] TextMeshPro sunText;
    [SerializeField] TextMeshPro levelText;
    [SerializeField] TextMeshPro scoreText;
    private int currentLevel = 1;
    private int currentExp = 0;
    private int expToNextLevel = 100;
    [SerializeField] private float startHealth;
    private int water = 0;
    private int sun = 0;
    private int score = 0;

    void Start()
    {
        InitHealth(startHealth, startHealth);
        UpdateUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            mousePosition.z = -2;
            SingletonGame.Instance.ExperienceManager.SpawnBatch(5, mousePosition);
        }

        LevelUp();
    }

    private void LevelUp() {
        if(currentExp > expToNextLevel){
            currentLevel +=1;
            currentExp -= expToNextLevel;
            expToNextLevel += 50;
            UpdateUI();
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
        SingletonGame.Instance.PlayerManager.OnPlayerDied();
    }
}
