using System.Collections;
using System.Collections.Generic;
using Plant;
using UnityEngine;

public class PlantDataDetectorAdapter : PlantDataAdapter
{
    void Start()
    {
        transform.localScale = new Vector3(Data.range, Data.range, 1);
    }
}
