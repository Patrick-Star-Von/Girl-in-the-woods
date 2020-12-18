using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    public Stat health,defense,attack;

    public virtual void Start()
    {
        health.SetValue(10);
        defense.SetValue(5);
        attack.SetValue(5);
    }

    void Update()
    {
        
    }
}
