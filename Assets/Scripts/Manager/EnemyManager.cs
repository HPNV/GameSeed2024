using System.Collections.Concurrent;
using System.Collections.Generic;
using Enemy;
using Unity.VisualScripting;
using UnityEngine;

namespace Manager
{
    public class EnemyManager
    {
        private Queue<EnemyBehaviour> _enemyPool;
        private GameObject _enemyPrefab;
        private Dictionary<EnemyName, EnemyData> _enemyData;

        public void Initialize()
        {
            _enemyPool = new Queue<EnemyBehaviour>();
            _enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy");
            
            _enemyData = new Dictionary<EnemyName, EnemyData>
            {
                {EnemyName.SludgeGrunt, Resources.Load<EnemyData>("Enemy/Sludge Grunt")},
                {EnemyName.SlimeSpitter, Resources.Load<EnemyData>("Enemy/Slime Spitter")},
                {EnemyName.BlastBlob, Resources.Load<EnemyData>("Enemy/Blast Blob")},
                {EnemyName.SwiftSlimer, Resources.Load<EnemyData>("Enemy/Swift Slimer")},
                {EnemyName.GooGuardian, Resources.Load<EnemyData>("Enemy/Goo Guardian")},
                {EnemyName.GlobLobber, Resources.Load<EnemyData>("Enemy/Glob Lobber")},
                {EnemyName.GelGrenadier, Resources.Load<EnemyData>("Enemy/Gel Grenadier")},
                {EnemyName.GoliathOoze, Resources.Load<EnemyData>("Enemy/Goliath Ooze")}
            };
        }
        
        public GameObject Spawn(EnemyName name, Vector2 position, float multiplier)
        {
            if (_enemyPool.Count == 0)
            {
                var enemyObject = Object.Instantiate(_enemyPrefab, position, Quaternion.identity);
                var enemyBehaviour = enemyObject.GetComponent<EnemyBehaviour>();
                enemyBehaviour.enemyData = _enemyData[name];
                enemyBehaviour.enemyData.health *= multiplier;
                enemyBehaviour.enemyData.attackPower *= multiplier;
                enemyBehaviour.enemyData.movementSpeed *= multiplier;
                return enemyObject;
            }
            
            var enemy = _enemyPool.Dequeue();
            
            enemy.transform.position = position;
            enemy.enemyData = _enemyData[name];
            enemy.enemyData.health *= multiplier;
            enemy.enemyData.attackPower *= multiplier;
            enemy.enemyData.movementSpeed *= multiplier;

            enemy.gameObject.SetActive(true);
            enemy.Initialize();
            
            return enemy.gameObject;
        }
        
        public void Despawn(EnemyBehaviour enemy)
        {
            enemy.gameObject.SetActive(false);
            enemy.enemyData = enemy.baseData;
            _enemyPool.Enqueue(enemy);
        }

        
    }
}