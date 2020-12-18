using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{

    public Transform jumpTransform;//检测是否跳起
    
    Rigidbody myForce;


    Animator anim;
    //Vector3 climbPlace;//攀爬目的地
    float horizontal;
    float vertical;
    
    float moveLimit = 1;
    float squat;
    bool isAir;
    int squatFlag = 1;
    float height;
    CapsuleCollider capsule;


    [HideInInspector]
    public int walk = 0;
    // 0 --> 静止
    // 1 --> 大致往左
    // 2 --> 大致往右
    [HideInInspector]
    public bool pullFlag = false;

    List<Vector3> boxPosition;
    Quaternion playerRotation;

    private void Awake()
    {
        //cam = Camera.main;
        myForce = GetComponent<Rigidbody>();
    }


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        capsule = GetComponent<CapsuleCollider>();
    }




    private void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
          vertical = Input.GetAxis("Vertical");

        Vector3 a = transform.position;
        if (!pullFlag)
        {
            a.x += horizontal * 0.1f * moveLimit * squat;
            a.z += vertical * 0.1f * moveLimit * squat;
            transform.position = a;
        }
        PlayerManager.instance.isAir = isAir = !Physics.Raycast(jumpTransform.position, -transform.up, 0.1f);
        if (Input.GetButtonDown("Jump") && !isAir)
        {
            myForce.AddForce(new Vector3(0, 3 * myForce.mass, 0), ForceMode.Impulse);
        }
        



        if (horizontal > 0.5f)
        {
            walk = 2;
            if (vertical > 0.5f)
            {
                playerRotation.eulerAngles = new Vector3(0f, 45f, 0f);
                moveLimit = 0.707f;
            }
            else if (vertical < -0.5f)
            {
                playerRotation.eulerAngles = new Vector3(0f, 135f, 0f);
                moveLimit = 0.707f;
            }
            else
            {
                playerRotation.eulerAngles = new Vector3(0f, 90f, 0f);
                moveLimit = 1f;
            }
        }
        else if (horizontal < -0.5f)
        {
            walk = 1;
            if (vertical > 0.5f)
            {
                playerRotation.eulerAngles = new Vector3(0f, -45f, 0f);
                moveLimit = 0.707f;
            }
            else if (vertical < -0.5f)
            {
                playerRotation.eulerAngles = new Vector3(0f, -135f, 0f);
                moveLimit = 0.707f;
            }
            else
            {
                playerRotation.eulerAngles = new Vector3(0f, -90f, 0f);
                moveLimit = 1f;
            }
        }
        else
        {
            if (vertical > 0.5f)
            {
                playerRotation.eulerAngles = new Vector3(0f, 0f, 0f);
                moveLimit = 1f;
                walk = 2;
            }
            else if (vertical < -0.5f)
            {
                playerRotation.eulerAngles = new Vector3(0f, 180f, 0f);
                moveLimit = 1f;
                walk = 1;
            }
            
        }

        

    }
    // Update is called once per frame
    
    void Update()
    {

        if (horizontal > 0.5f || horizontal < -0.5f || vertical > 0.5f || vertical < -0.5f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, playerRotation, Time.deltaTime * 5);
            anim.SetBool("isWalking", true);
        }
        else
        {
            walk = 0;
            anim.SetBool("isWalking", false);
        }

        if (Input.GetButtonDown("squat"))//按一下，再按一下，拉杆构造
        {
            squatFlag *= -1;
        }


        if (squatFlag == 1)
        {
            //站
            squat = 1f;
            height = 1.6f;
            
        }
        else
        {
            //蹲
            squat = 0.35f;
            height = 0.9f;
        }
        capsule.height = height;
        capsule.center = new Vector3(0, height / 2, 0);




    }





}
