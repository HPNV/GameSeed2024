using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0.05f;
    [SerializeField]
    private float rotateSpeed = 0.05f;
    [SerializeField]
    private float health = 100; 
  

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }
}
