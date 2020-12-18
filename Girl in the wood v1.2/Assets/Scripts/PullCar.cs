using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullCar : MonoBehaviour
{
    public GameObject car;
    public GameObject player;
    bool playerIn;
    bool stuck;
    float j_key;
    float angle;
    int turn;
    Vector3 pullDirection;
    void Start()
    {
        pullDirection = transform.position - car.transform.position;
        pullDirection.Normalize();
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            angle = Vector3.Angle(player.transform.position - car.transform.position, car.transform.forward);
            turn = angle > 90 ? 1 : 2;
            // 1 ---> 左边
            // 2 ---> 右边
            playerIn = true;
        }
        else if (other.tag == "Wall")
        {
            stuck = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            j_key = Input.GetAxis("J_Key");
            int walk = player.GetComponent<PlayerMove>().walk;
            if (j_key > 0.5f && walk == turn)
            {
                player.GetComponent<PlayerMove>().pullFlag = true;
                if (!stuck)
                {
                    car.transform.position += pullDirection * 0.01f;
                    player.transform.position += pullDirection * 0.01f;
                }

            }
            else
            {
                player.GetComponent<PlayerMove>().pullFlag = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIn = false;
        }
        else if (other.tag == "Wall")
        {
            stuck = false;
        }
    }
}
