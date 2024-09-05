using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SelectorService : MonoBehaviour
{

    public void Place()
    {
        if (!Validate()) return;
        OnPlace();
        AfterPlace();
    }

    protected abstract bool Validate();
    protected abstract void OnPlace();
    protected abstract void AfterPlace();
}
