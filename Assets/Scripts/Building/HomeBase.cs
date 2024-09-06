using System.Collections.Generic;
using System.Linq;
using Building;
using TMPro;
using UnityEngine;
using Utils;

public class HomeBase : MonoBehaviour
{
    [SerializeField] Bar expBar;
    [SerializeField] Bar HpBar;
    [SerializeField] TextMeshPro waterText;
    [SerializeField] TextMeshPro sunText;
    private int currentLevel = 1;
    private int currentExp = 0;
    private int expToNextLevel = 100;
    private float CurrentHealth = 100f;
    private float maxHealth = 100f;
    private int water = 0;
    private int sun = 0;


    void Start()
    {
        UpdatetUI();
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
            UpdatetUI();
            SingletonGame.Instance.SpawnPlant();
        }
    }

    public void GainExp(int exp) {
        currentExp += exp;
        UpdatetUI();
    }

    public void UpdatetUI() {
        expBar.Exp = currentExp;
        expBar.setMaxValue(expToNextLevel);
        HpBar.Exp = CurrentHealth;
        HpBar.setMaxValue(100);
        waterText.text = water.ToString();
        sunText.text = sun.ToString();
    }

    public void TakeDamage(float damage) {
        CurrentHealth -= damage;
        UpdatetUI();
        if(CurrentHealth <= 0) {
            
        }
    }

    public void addSun(int sun) {
        this.sun += sun;
    }

    public void addWater(int water) {
        this.water += water;
    }
}
