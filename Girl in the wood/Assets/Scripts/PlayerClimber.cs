using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimber : MonoBehaviour
{
    public Transform headTransform;//检测头顶的射线点
    public Transform climbTransform;//检测攀爬的射线起点

    RaycastHit climbTopHit;
    RaycastHit climbDownHit;
    float horizontal;
    float vertical;
    Vector3 climbPlace;//攀爬目的地
    Vector3 pointPosition;
    Vector3 playerPosition;
    Quaternion playerRotation;
    bool spaceDown;
    bool flag;
    bool suspension = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        spaceDown = PlayerManager.instance.isAir && Input.GetButtonDown("Jump");
        Physics.Raycast(climbTransform.position, -transform.up, out climbDownHit);
        float cliffHeight = climbDownHit.point.y - transform.position.y;
        if (cliffHeight < 2.5f && cliffHeight > 0f && spaceDown && !suspension && !Physics.Raycast(headTransform.position, transform.up, out climbTopHit, 0.85f))
        {
            //悬挂
            suspension = true;
            pointPosition = climbDownHit.point;
            playerPosition = transform.position;
            playerPosition.y = pointPosition.y - 1.3f;
        }
        //print("spaceDown:" + spaceDown);
        if (suspension)
        {
            transform.SetPositionAndRotation(playerPosition, transform.rotation);
        }

        if (suspension && Input.GetAxis("Vertical") > 0.01f)
        {
            //print(!Physics.Raycast(headTransform.position, transform.up, out climbTopHit, 2f)+","+spaceDown);
            if (!Physics.Raycast(headTransform.position, transform.up, out climbTopHit, 2f) && suspension)
            {
                climbPlace = climbTransform.position;
                climbPlace.y = (float)(climbDownHit.transform.localScale.y * 0.5 + climbDownHit.transform.position.y);
                transform.SetPositionAndRotation(climbPlace, transform.rotation);
                //print("nice");
            }
            else if (suspension)
            {
                print("爬不了");
            }
            suspension = false;
        }
        else if (cliffHeight < -1f && cliffHeight > -5f && suspension)
        {
            //print("跳");
            suspension = false;
        }
        //print("suspension : " + suspension);
        if (horizontal > 0.9f || horizontal < -0.9f ||vertical < -0.9f)
        {//结束悬挂
            suspension = false;
        }

    }
}
