using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlStat : CharacterStat
{
    float san, maxSan;
    SRA recover;

    public override void Start()
    {
        maxSan = 100f;
        health.SetValue(1);
        defense.SetValue(0);
        san = maxSan;
    }

    void Update()
    {
        RecoverSan();
    }

    void RecoverSan(){
        // Calculate San based on SRA
        float calculatedSan = 100f;
        // If san as usual
        
        // If san less than 60
        if(san < 60){
            
        }
        // Set calculated value to current san
        san = calculatedSan;
    }
}
