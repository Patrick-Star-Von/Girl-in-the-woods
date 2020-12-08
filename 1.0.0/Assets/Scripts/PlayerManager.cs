using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    
    public static PlayerManager instance;
    public GameObject player;
    public bool isAir;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        isAir = false;
    }




    enum PlayerState
    {

    }

}
