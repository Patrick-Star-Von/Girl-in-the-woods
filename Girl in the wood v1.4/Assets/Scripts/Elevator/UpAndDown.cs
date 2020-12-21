using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDown : MonoBehaviour
{
    public Transform up_;
    public Transform down_;
    public GameObject elevator;
    bool flag = true;
    float up, down;
    bool up_down = false;

    public float 电梯速度;

    Vector3 position;
    private void Start()
    {
        position = elevator.transform.position;
        up = up_.position.y;
        down = down_.position.y;
    }

    private void Update()
    {
        

        if (up_down && position.y < up)
        {
            position.y += 电梯速度;
            elevator.transform.position = position;
        }

        if(!up_down && position.y > down)
        {
            position.y -= 电梯速度;
            elevator.transform.position = position;
        }

        if(Input.GetKeyDown(KeyCode.F) == flag)
        {
            flag = true;
        }
    }



    private void OnTriggerStay(Collider other)
    {
        Transform player = other.transform;
        Vector3 tempPosition = transform.position;
        tempPosition.y = player.transform.position.y;

        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.F) && Vector3.Angle(tempPosition - player.position, player.forward) < 45f && flag)
        {
            flag = false;
            if (!up_down)
            {
                up_down = true;
            }
            else
            {
                up_down = false;
            }

        }
        print(Input.GetKeyDown(KeyCode.F));
    }



}
