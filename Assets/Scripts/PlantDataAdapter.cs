using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantDataAdapter : MonoBehaviour
{
    private PlantData data;
    void Start()
    {
        data = transform.parent.gameObject.GetComponent<PlantData>();
        transform.localScale = new Vector3(data.range, data.range, 1);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
