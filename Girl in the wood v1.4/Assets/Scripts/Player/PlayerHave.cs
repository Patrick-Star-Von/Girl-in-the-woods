using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHave : MonoBehaviour
{
    public static PlayerHave ins;
    [HideInInspector]
    public bool haveStone;
    private void Awake()
    {
        if(ins == null)
        {
            ins = this;
        }
        
    }
    private void Start()
    {
        haveStone = false;
    }

}
