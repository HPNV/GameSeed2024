using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeBase : MonoBehaviour
{
    private int currentLevel = 1;
    private int currentEXP = 0;
    private int expToNextLevel = 100;

    void Start()
    {
        
    }

    void Update()
    {
            
    }

    private void LevelUp() {
        if(currentEXP > expToNextLevel){
            currentLevel +=1;
            currentEXP -= expToNextLevel;
            expToNextLevel += 50;
        }
    }
}
