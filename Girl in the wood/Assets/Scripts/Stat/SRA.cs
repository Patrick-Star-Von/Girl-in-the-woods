using System;
using System.Collections;
using UnityEngine;

public class SRA
{
    private int currentValue, maxValue, minValue;

    void Start()
    {
        maxValue = 5;
        minValue = 1;
        currentValue = maxValue;
    }

    void Update()
    {

    }

    public void SetValue(int value){
        value = Mathf.Clamp(value, minValue, maxValue);

        currentValue = value;
    }
}
