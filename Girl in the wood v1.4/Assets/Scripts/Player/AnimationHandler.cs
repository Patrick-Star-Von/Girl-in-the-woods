using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationHandler : MonoBehaviour
{
    Animator anim;
    PlayerMove playerPara;
    float speed;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerPara = PlayerMove.S;
    }

    void Update()
    {
        speed = Mathf.Max(Mathf.Abs(playerPara.horizontal), Mathf.Abs(playerPara.vertical));
        anim.SetFloat("Speed",speed,0.1f,Time.deltaTime);
    }
}
