using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SelectorService : MonoBehaviour
{

    public void Place()
    {
        OnPlace();
        AfterPlace();
    }

    protected abstract void OnPlace();
    protected abstract void AfterPlace();
}
