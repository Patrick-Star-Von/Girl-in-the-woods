using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushCar : MonoBehaviour
{
    public GameObject car;
    public GameObject player;
    bool playerIn;
    [HideInInspector]
    public bool stuck = false;
    float j_key;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        j_key = Input.GetAxis("J_Key");
        int walk = player.GetComponent<PlayerMove>().walk;
        if (!stuck)
        {
            if (j_key > 0.5f && playerIn && walk == 2)
            {
                car.transform.position += transform.forward * 0.01f;
            }
            else if (j_key > 0.5f && playerIn && walk == 1)
            {
                car.transform.position -= transform.forward * 0.01f;
            }
        }
        
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {

            playerIn = true;
        }
        else if(other.tag == "Wall")
        {
            stuck = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIn = false;
        }
        else if(other.tag == "Wall")
        {
            stuck = false;
        }
    }
}
