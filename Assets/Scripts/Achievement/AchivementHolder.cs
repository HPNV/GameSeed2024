using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AchivementHolder : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private TextMeshPro AchievementName;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAchievement(Sprite sprite, string name)
    {
        this.sprite.sprite = sprite;
        AchievementName.text = name;
    }
}
