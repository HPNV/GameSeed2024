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
            {EnemyType.Ranged, Resources.Load<EnemyData>("Enemy/Ranged")}
        };
    }

    
    void Update()
    {
        // TEMPORARY
        var camera = Camera.main;
        var cursorPosition = camera!.ScreenToWorldPoint(Input.mousePosition);
        cursorPosition.z = -0.8f;    
        
        transform.position = cursorPosition;
        
        if (Input.GetKeyDown(KeyCode.M))
        {
            var x = Random.Range(-10, 10);
            var y = Random.Range(-10, 10);
            
            var position = transform.position;
            var enemy = Instantiate(enemyObject, new Vector3(position.x + x, position.y + y, position.z), Quaternion.identity);
            var behaviour = enemy.GetComponent<EnemyBehaviour>();
            behaviour.enemyData = _enemyData[EnemyType.Melee];
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            var x = Random.Range(-10, 10);
            var y = Random.Range(-10, 10);
            
            var position = transform.position;
            var enemy = Instantiate(enemyObject, new Vector3(position.x + x, position.y + y, position.z), Quaternion.identity);
            var behaviour = enemy.GetComponent<EnemyBehaviour>();
            behaviour.enemyData = _enemyData[EnemyType.Ranged];
        }
    }
}
