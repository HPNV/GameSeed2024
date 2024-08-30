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
        var camera = Camera.main;
        var cursorPosition = camera!.ScreenToWorldPoint(Input.mousePosition);
        cursorPosition.z = -0.8f;    
        
        transform.position = cursorPosition;
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            var x = Random.Range(-10, 10);
            var y = Random.Range(-10, 10);
            
            var position = transform.position + new Vector3(x, y, 0);
            Instantiate(enemyObject, transform);
        }
    }
}
