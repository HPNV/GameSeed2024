using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

public class DummyAI : MonoBehaviour
{
    [SerializeField] private GameObject enemyObject;
    private Dictionary<EnemyType, EnemyData> _enemyData;
    void Start()
    {
        _enemyData = new Dictionary<EnemyType, EnemyData>
        {
            {EnemyType.Melee, Resources.Load<EnemyData>("Enemy/Melee")},
            {EnemyType.Ranged, Resources.Load<EnemyData>("Enemy/Ranged")},
            {EnemyType.Explosive, Resources.Load<EnemyData>("Enemy/Explosive")}
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
