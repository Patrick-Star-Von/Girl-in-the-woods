using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector]
    public static PlayerManager instance;
    public GameObject player;
    [HideInInspector]
    public bool isAir;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        isAir = false;
    }

}
