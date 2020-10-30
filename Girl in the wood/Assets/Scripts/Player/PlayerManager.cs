using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton

    public static PlayerManager S;

    void Awake(){
        if(S != null){
            return;
        }

        S = this;
    }

    #endregion

    public GameObject player;
    public bool isInAir;
    public bool isGrounded;

    void Start()
    {
        isInAir = false;
        isGrounded = true;
    }
}
