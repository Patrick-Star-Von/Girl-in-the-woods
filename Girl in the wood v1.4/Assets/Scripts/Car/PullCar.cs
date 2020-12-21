using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullCar : MonoBehaviour
{
    public GameObject car;
    public GameObject player;
    public Transform LPoint, RPoint;
    public Transform eyeTransform;
    public float 推行速度;
    float j_key;
    Vector3 pullDirection;
    void Start()
    {
        pullDirection = transform.position - car.transform.position;
        pullDirection.Normalize();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            j_key = Input.GetAxis("J_Key");

            if(j_key > 0.5f)
            {
                Vector3 carPoint = car.transform.position;
                Vector3 eyePoint = eyeTransform.position;
                eyePoint.y = 0;
                carPoint.y = 0;
                player.transform.LookAt(Vector3.Lerp(eyePoint, carPoint, 0.1f));
                player.transform.position = Vector3.Lerp(player.transform.position, transform.position, 0.1f);
                player.GetComponent<PlayerMove>().pullFlag = true;
                if(!(Vector3.Distance(carPoint,LPoint.position) + Vector3.Distance(carPoint, RPoint.position) > 0.1f + Vector3.Distance(LPoint.position, RPoint.position)))
                {
                    car.transform.position += car.transform.forward * 0.01f * Input.GetAxis("Horizontal");
                }
                else
                {
                    if(Vector3.Distance(LPoint.position,carPoint) > Vector3.Distance(RPoint.position, carPoint))
                    {
                        if(Input.GetAxis("Horizontal") < 0)
                        {
                            car.transform.position += car.transform.forward * 推行速度 * Input.GetAxis("Horizontal");
                        }
                        
                    }
                    else
                    {
                        if (Input.GetAxis("Horizontal") > 0)
                        {
                            car.transform.position += car.transform.forward * 推行速度 * Input.GetAxis("Horizontal");
                        }
                    }
                }
            }
            else
            {
                player.GetComponent<PlayerMove>().pullFlag = false;
            }

        }
    }
}
