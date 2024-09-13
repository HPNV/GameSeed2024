using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoseScreen : MonoBehaviour
{   
    [SerializeField] private TextMeshPro ScoreValue;
    [SerializeField] private TextMeshPro EnemyKilledValue;
    [SerializeField] private TextMeshPro PlantPlantedValue;
    [SerializeField] private TextMeshPro TimeValue;

    public string time;

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
        TimeValue.text = time;
    }
}
