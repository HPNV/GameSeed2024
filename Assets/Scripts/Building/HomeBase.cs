using System.Collections.Generic;
using System.Linq;
using Building;

using UnityEngine;
using Utils;

public class HomeBase : MonoBehaviour
{
    [SerializeField] Bar expBar;
    [SerializeField] Bar HpBar;
    private int currentLevel = 1;
    private int currentExp = 0;
    private int expToNextLevel = 100;
    private float CurrentHealth = 100f;
    private float maxHealth = 100f;


    void Start()
    {
        UpdatetUIpBar();
    }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                mousePosition.z = -2;
                SingletonGame.Instance.ExperienceManager.Spawn(1,mousePosition);
            }

            LevelUp();
        }

    private void LevelUp() {
        if(currentExp > expToNextLevel){
            currentLevel +=1;
            currentExp -= expToNextLevel;
            expToNextLevel += 50;
            SingletonGame.Instance.SpawnPlant();
        }
    }

    public void GainExp(int exp) {
        currentExp += exp;
        UpdatetUIpBar();
    }

    public void UpdatetUIpBar() {
        expBar.Exp = currentExp;
        expBar.setMaxValue(expToNextLevel);
        HpBar.Exp = CurrentHealth;
        HpBar.setMaxValue(100);
    }

    public void TakeDamage(float damage) {
        CurrentHealth -= damage;
        UpdatetUIpBar();
        if(CurrentHealth <= 0) {

        }
    }
}
