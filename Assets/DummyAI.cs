using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

public class DummyAI : MonoBehaviour
{
    [SerializeField] private GameObject enemyObject;
    private Dictionary<EnemyName, EnemyData> _enemyData;
    void Start()
    {
        _enemyData = new Dictionary<EnemyName, EnemyData>
        {
            {EnemyName.SludgeGrunt, Resources.Load<EnemyData>("Enemy/Melee")},
            {EnemyName.SlimeSpitter, Resources.Load<EnemyData>("Enemy/Ranged")},
            {EnemyName.BlastBlob, Resources.Load<EnemyData>("Enemy/Explosive")}
        };
    }

    
    void Update()
    {
        // TEMPORARY
        var camera = Camera.main;
        var cursorPosition = camera!.ScreenToWorldPoint(Input.mousePosition);
        cursorPosition.z = -0.8f;    
        
        transform.position = cursorPosition;
        
        
    }
}
