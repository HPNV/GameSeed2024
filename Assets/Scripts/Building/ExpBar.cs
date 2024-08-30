using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    [SerializeField] private Slider currentExpBar;

    private int currentExp;

    public int Exp{
        get => currentExp;
        set {
            currentExp = value;
        }
    }

    void Start()
    {
        currentExpBar.value = 0;
    }

    void Update()
    {
        currentExpBar.value = currentExp;
    }

    public void setMaxExp(int maxExp) {
        currentExpBar.maxValue = maxExp;
    }
}
