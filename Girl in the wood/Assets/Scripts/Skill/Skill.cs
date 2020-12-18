using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{

    
    public GameObject BOX;
    public GameObject ENEMY;
    public GameObject fireBallExample;
    public GameObject iceBallExample;
    Rigidbody[] boxs;//可拖拽物体集合
    Transform[] boxsTransform;//可拖拽物体位置集合

      EnemyAI[] enemys;//敌人脚本集合
    Transform[] enemysTransform;//敌人位置集合
        EnemyAI tEnemy;
     FixedJoint fixedJoint;
      
    float j_key;
    int count;

    float skillTime_1 = 0f;
    float skillTime_2 = 0f;
    float skillTime_3 = 0f;
    bool skillFlag_3 = true;
    bool skillFlag_4 = true;


    GameObject fireBall;
    GameObject iceBall;

    RaycastHit hit1;
    
    // Start is called before the first frame update
    void Start()
    {
        boxs = BOX.GetComponentsInChildren<Rigidbody>();//获取到可拖拽物体的刚体
        boxsTransform = BOX.GetComponentsInChildren<Transform>();//获取到可拖拽物体的位置
        enemys = ENEMY.GetComponentsInChildren<EnemyAI>();//获取到敌人的脚本
        enemysTransform = ENEMY.GetComponentsInChildren<Transform>();//获取到敌人的位置
        //print("enemys.Length is " + enemys.Length);
    }

    


    // Update is called once per frame
    void Update()
    {
        //j_key = Input.GetAxis("J_Key");
        //if (j_key > 0.5f && !graspFlag)
        //{
        //    float min = 2;
        //    count = 1;
        //    Rigidbody minBox = null;
        //    foreach (Rigidbody i in boxs)
        //    {
        //        float dis = Vector3.Distance(transform.position, boxsTransform[count].position);
        //        if (dis < min)
        //        {
        //            min = dis;
        //            minBox = i;
        //        }
        //        count++;
        //    }
        //    if (minBox != null)
        //    {
        //        fixedJoint = gameObject.AddComponent<FixedJoint>();
        //        fixedJoint.connectedBody = minBox;
        //        graspFlag = true;
        //    }
        //}
        //else if (j_key < 0.5f)
        //{
        //    if (graspFlag)
        //    {
        //        Destroy(gameObject.GetComponent<FixedJoint>());
        //        graspFlag = false;
        //    }
        //}
        //j_key = Input.GetAxis("J_Key");
        //if (j_key > 0.5f)
        //{
            
        //}
        //else if (j_key < 0.5f)
        //{
            
        //}



        if (Input.GetButtonDown("1_Key") && Time.time > skillTime_1)
        {
            skillTime_1 = Time.time + 3;
            //print("skill 1");
            count = 1;
            foreach (EnemyAI i in enemys)
            {
                if (i.SEE_ME && i.IN_DISTANCE)
                {
                    StartCoroutine(i.Skill_1());
                }
                else if (i.NO_WALL && i.IN_DISTANCE)
                {
                    i.FIND_TARGET = true;
                }
            }
        }
        else if (Input.GetButtonDown("2_Key") && Time.time > skillTime_2)
        {
            
            Vector3 a = GetPointForScreen();
            a.y -= 1f;
            transform.LookAt(a);
            //Vector3 b = transform.position;
            //a.y = 0;
            //b.y = 0;
            //Vector3 c = a - b;
            //c.Normalize();
            //Rotate(transform, c.x, c.z, 1f);

            skillTime_2 = Time.time + 3.5f;
            Instantiate(fireBallExample, transform.GetChild(0).GetChild(0).position + transform.forward * 0.5f, transform.rotation);
        }
        else if (Input.GetButtonDown("3_Key"))
        {
            if (skillFlag_3 && Time.time > skillTime_3)
            {

                skillFlag_3 = false;
                Vector3 iceBallPosition = GetPointForScreen() - transform.position;
                if(iceBallPosition.magnitude > 10f)
                {
                    iceBallPosition.Normalize();
                    iceBallPosition *= 10f;
                }
                iceBallPosition.y += 10f;
                iceBall = Instantiate(iceBallExample, transform.position + iceBallPosition, transform.rotation);
            }
            else if (!skillFlag_3)
            {
                skillTime_3 = Time.time + 4f;
                Destroy(iceBall);
                skillFlag_3 = true;
            }

        }
        else if (Input.GetButtonDown("4_Key"))
        {

            if (skillFlag_4)
            {
                float min = float.MaxValue;
                foreach (EnemyAI i in enemys)
                {
                    float dis = Vector3.Distance(i.transform.position, GetPointForScreen());
                    if (dis < min)
                    {
                        min = dis;
                        tEnemy = i;
                    }
                }
                if (min > 3f)
                {
                    goto skill_failure;
                }
                tEnemy.nav.speed = 0f;
                tEnemy.light.color = Color.black;
                tEnemy.enabled = false;
                skillFlag_4 = false;
            }
            else
            {
                tEnemy.alertValue = 0f;
                tEnemy.nav.speed = 2.5f;
                tEnemy.light.color = Color.white;
                tEnemy.enabled = true;
                skillFlag_4 = true;
            }

        }skill_failure:; 
    




    }

    public Vector3 GetPointForScreen()
    {
        RaycastHit ScreenToWorld_hit;
        Ray ray = CameraManager.instance.newCam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out ScreenToWorld_hit))
        {
            return ScreenToWorld_hit.point;
        }
        return Vector3.zero;
    }


    public void Rotate(Transform transform, float horizontal, float vertical, float fRotateSpeed)
    {
        Vector3 targetDir = new Vector3(horizontal, 0, vertical);
        if (targetDir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, fRotateSpeed);
        }
    }
}
