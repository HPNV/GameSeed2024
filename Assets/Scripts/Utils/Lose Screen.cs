using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoseScreen : MonoBehaviour
{   
    [SerializeField] private TextMeshPro ScoreValue;
    [SerializeField] private TextMeshPro EnemyKilledValue;
    [SerializeField] private TextMeshPro PlantPlantedValue;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void UpdateUI(int score,int enemyKilled, int plantPlanted) {
        ScoreValue.text = score.ToString();
        EnemyKilledValue.text = enemyKilled.ToString();
        PlantPlantedValue.text = plantPlanted.ToString();
    }
}
