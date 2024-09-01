using System.Collections.Generic;
using System.Linq;
using Building;

using UnityEngine;
using Utils;

public class HomeBase : MonoBehaviour
{
    [SerializeField] ExpBar expBar;
    private int currentLevel = 1;
    private int currentExp = 0;
    private int expToNextLevel = 100;
    private float health = 100f;


    void Start()
    {
        SetExpBar();
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
        
            SetExpBar();
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
        }


    public void SetExpBar() {
        expBar.Exp = currentExp;
        expBar.setMaxExp(expToNextLevel);
    }
}
