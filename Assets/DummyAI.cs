using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyAI : MonoBehaviour
{
    [SerializeField] private GameObject enemyObject;
    void Start()
    {
        
    }

    
    void Update()
    {
        // TEMPORARY
        var camera = Camera.main;
        var cursorPosition = camera!.ScreenToWorldPoint(Input.mousePosition);
        cursorPosition.z = -0.8f;    
        
        transform.position = cursorPosition;
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            var x = Random.Range(-10, 10);
            var y = Random.Range(-10, 10);
            
            var position = transform.position;
            Instantiate(enemyObject, new Vector3(position.x, position.y, position.z), Quaternion.identity);
        }
    }
}
