using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shine : MonoBehaviour
{
    public static Shine instance;
    public static bool shining;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        shining = false;
    }

    public void StartShining( )
    {
        shining = true;

    }

    public void OffShining()
    {
        shining = false;
    }


}
