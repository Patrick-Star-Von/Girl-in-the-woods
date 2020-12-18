using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeAI : MonoBehaviour
{
    //公开变量
    public Transform routes;//路径集合


    //协程返回布尔声明
    bool moveto_;

    //协程变量声明
    Coroutine moveto = null;

    //特殊变量声明
    NavMeshAgent nav;
    

    //一般变量声明
    Vector3[] points;//存储所有巡逻点
    Vector3 nextTarget;//下一个巡逻点
    int point_i;//单个巡逻点的索引


    private void Awake()
    {//变量初始化
        nav = GetComponent<NavMeshAgent>();
        points = new Vector3[routes.childCount];//初始化路径集合
    }


    // Start is called before the first frame update
    void Start()
    {//数据初始化

        for (int i = 0; i < routes.childCount; i++)
        {//获取路径集合
            points[i] = routes.GetChild(i).position;
            points[i].y = transform.position.y;
        }
        //toturnto = StartCoroutine(ToTurnTo(90));
    }

    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        nextTarget = points[point_i % points.Length];
        if (moveto == null)
        {
            moveto = StartCoroutine(MoveTo(nextTarget));//巡逻
        }
        else
        {
            
            if (moveto_)
            {//到达终点
                moveto = null;
                point_i++;
            }
        }
    }






    IEnumerator ToTurnTo(float angle, float limAngle = 5f)
    {//参数：角度，转速
        float i = angle / limAngle;
        while(i-- > 0)
        {
            print(i);
            transform.Rotate(Vector3.up, limAngle);
            yield return 0;
        }
        yield break;
    }




    IEnumerator MoveTo(Vector3 target,float speed = 3.5f)
    {//参数：坐标，速度
        bool flag = true;
        nav.speed = speed;
        moveto_ = false;
        while ( Vector3.Distance(transform.position,target) > 3f )
        {
            if (flag)
            {
                nav.SetDestination(target);
                flag = false;
            }
            yield return 0;
        }
        moveto_ = true;
        yield break;
    }








}
