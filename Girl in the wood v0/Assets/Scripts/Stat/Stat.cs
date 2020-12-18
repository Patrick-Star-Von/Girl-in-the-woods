using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat
{
    private int baseValue;
    int finalValue;
    
    private List<int> modifiers;

    void Start(){
        modifiers = new List<int>();
        SetValue(5);
    }

    public void SetValue(int value){
        baseValue = value;
    }

    public int GetValue(){
        finalValue = baseValue;
        modifiers.ForEach(x => finalValue += x);
        return finalValue;
    }

    void AddModifier(int modifier){
        if(modifier != 0){
            modifiers.Add(modifier);
        }
    }

    void RemoveModifier(int modifier){
        if(modifier != 0){
            modifiers.Remove(modifier);
        }
    }
}
