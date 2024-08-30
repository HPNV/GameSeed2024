using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Manager;
using UnityEditor;
using UnityEngine;

public class HomeBase : MonoBehaviour
{
    [SerializeField] ExpBar expBar;
    private int currentLevel = 1;
    private int currentExp = 0;
    private int expToNextLevel = 100;
    private SpriteRenderer spriteRenderer;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>("yggdrasil_0");
        SetExpBar();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            mousePosition.z = -2;
            ExperienceManager.Spawn(1,mousePosition);
        }
        
        SetExpBar();
        LevelUp();
    }

    private void LevelUp() {
        if(currentExp > expToNextLevel){
            currentLevel +=1;
            currentExp -= expToNextLevel;
            expToNextLevel += 50;
            SIngletonGame.Instance.SpawnPlant();
        }
    }

    public void GainExp(int exp) {
        currentExp += exp;
    }


    public void SetExpBar() {
        expBar.Exp = currentExp;
        expBar.setMaxExp(expToNextLevel);
    }
}
