using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiMove : MonoBehaviour
{
    public Transform routes;//路径集合
    public Transform playerTransform;//玩家transform
    public Transform headTransform;
    public float pauseTime = 3.0f;//巡逻暂停时长
    public float alertTime = 3.0f;//警戒时长
    public float detectRange = 10.0f;//检测范围
    public float detectAngle = 120.0f;
  

    [HideInInspector]
    public bool FACE_TO_ME;//面对我？
    [HideInInspector]
    public bool WITHIN_THE_DISTANCE;//在我附近？
    [HideInInspector]
    public bool SEE_ME;//看见我？
    [HideInInspector]
    public bool ATTACK;


    StateMachine state;
    NavMeshAgent nav;//敌人自动寻路AI
    Light alertLight;//敌人头灯
    EnemyManager enemyManager;


    Vector3[] paths;//存储所有巡逻点
    Vector3 path;//暂时存储单个巡逻点
    Vector3 finPosition;//玩家最后出现在敌人视野里的位置
    Vector3 enemyPosition2;//敌人自身的位置
    Vector3 rayDirection2;//射线的向量角
    float time, time2;//定时判断变量
    float awarenessThreshold = 3f;//警惕上限
    float awareness = 0;
    bool flag_arrive = true;//是否到达下一个点
    bool flag_time = true;//是否到时
    bool flag_track = true;//是否跟踪到玩家出现的最后一个点
    //bool flag_turn = true;
    int path_i = 0;//巡逻点的索引
    //int turn_i = 0;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();//初始化敌人寻路AI
        paths = new Vector3[routes.childCount];//初始化路径集合
        alertLight = GetComponentInChildren<Light>();//初始化敌人头灯
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyManager = EnemyManager.instance;
        for(int i=0;i< routes.childCount ;i++)
        {//获取路径集合
            paths[i] = routes.GetChild(i).position;
            paths[i].y = transform.position.y;
        }
    }

    
    void Update()
    {
        enemyPosition2 = transform.GetChild(0).GetChild(3).position;//计算敌人位姿2
        //敌人到我的向量
        Vector3 enemyToPlayerVector = playerTransform.position - transform.GetChild(0).GetChild(0).position;
        //敌人正对的向量
        Vector3 enemyForwardVector = -transform.GetChild(0).GetChild(0).up;
        //距离之内
        WITHIN_THE_DISTANCE = Vector3.Distance(transform.position, playerTransform.position) < detectRange ? true : false;
        if (WITHIN_THE_DISTANCE)
        {
            //面向我
            FACE_TO_ME = Vector3.Angle(enemyForwardVector, enemyToPlayerVector) < detectAngle * 0.5f ? true : false;
            RaycastHit hit2;
            if (Physics.Raycast(enemyPosition2, headTransform.position - enemyPosition2, out hit2))
            {
                //看到我
                SEE_ME = hit2.transform == enemyManager.drivenTarget ? true : false;
                if (SEE_ME)
                {
                    finPosition = playerTransform.position;//记录玩家最后出现的点
                    awareness += Time.deltaTime;
                    if (awareness > awarenessThreshold)
                    {
                        ATTACK = true;
                    }
                }
                else
                {
                    awareness = 0;
                }
            }
        }
        else
        {
            FACE_TO_ME = false;
        }
        if(Vector3.Distance(finPosition, transform.position) < 2f)
        {
            ATTACK = false;
        }






        switch (Evaluate())
        {
            case StateMachine.Patrolling:
                
                break;
            case StateMachine.Vigilant:

                break;
            case StateMachine.Attack:

                break;
            case StateMachine.Searching:

                break;

        }


















        AiDecisionTree();
        




       

    }
    public void IsBlinded()//被闪光做出的动作
    {
        NavMeshAgent nav = GetComponent<NavMeshAgent>();
        Light light = GetComponentInChildren<Light>();


        if (nav != null && WITHIN_THE_DISTANCE && FACE_TO_ME)
        {
            nav.speed = 1f;
            nav.angularSpeed = 10f;
        }
        if (nav != null && WITHIN_THE_DISTANCE && !FACE_TO_ME)
        {
            nav.SetDestination(transform.position);
        }

    }




    void AiDecisionTree()
    {
        //---------------------------------------------------------------------------AI决策树----------------------------------------------------------------------------------//



        


        
        //Debug.Log(Time.time);
        if (WITHIN_THE_DISTANCE)
        {//（射线2射到了某个对象，对象是GFX）敌我双方无墙面阻挡
            if (Vector3.Distance(transform.position, playerTransform.position) < 4.0f)
            {//如果玩家在附近就转向直视
                transform.LookAt(Vector3.Lerp(transform.GetChild(0).GetChild(2).position, playerTransform.position, 0.2f));
            }

            if (FACE_TO_ME)
            {//（射线射到了某个对象，对象是body）已处于视野角内
                //Debug.Log(hit2.collider.gameObject.name);
                //-----------------------------攻击状态----------------------------------//
                flag_track = true;//设置为跟踪状态
                flag_arrive = false;//设置为不可巡逻状态
                alertLight.color = Color.red;//头灯变红
                finPosition = playerTransform.position;//记录玩家最后出现的点
                nav.SetDestination(finPosition);//敌人向那个点走去
                time2 = Time.time + alertTime;//存储时停时间
                //-----------------------------------------------------------------------//
            }
        }
        else if (Vector3.Distance(finPosition, transform.position) < 3.0f && flag_track)
        {//（距离最后发现玩家的点小于1m，还在处于跟踪状态）跟踪完成
            //-----------------------------跟踪状态----------------------------------//
            if (time2 < Time.time)
            {
                flag_track = false;//设置为非跟踪状态
                flag_arrive = true;//设置为可以巡逻状态
                flag_time = true;//设置为暂停时间未存储状态
                alertLight.color = Color.black;//头灯变黑
            }
            //-----------------------------------------------------------------------//
        }
        else
        {//（）未暴露视野
            //-----------------------------巡逻状态----------------------------------//
            if (flag_arrive)
            {
                path = paths[path_i++];//获取第i个巡逻点
                path_i %= paths.Length;
                nav.SetDestination(path);//走到第i个巡逻点
                flag_arrive = false;//设置为不可巡逻状态
            }
            else if (Vector3.Distance(transform.position, path) < 3.0f)
            {
                if (flag_time)
                {
                    time = Time.time + pauseTime;//设置暂停时间
                    flag_time = false;//设置为暂停时间已经存储状态
                }
                else if (time < Time.time)
                {
                    flag_arrive = true;//设置为可巡逻状态
                    flag_time = true;//设置为暂停时间未存储状态
                }
            }
            //-----------------------------------------------------------------------//
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------//

    }






    StateMachine Evaluate()
    {
        StateMachine currentState = StateMachine.Patrolling;
        if (ATTACK)
        {
            currentState = StateMachine.Attack;
        }
        else if (SEE_ME)
        {
            currentState = StateMachine.Vigilant;
        }
        else if(WITHIN_THE_DISTANCE)
        {
            currentState = StateMachine.Searching;
        }
        return currentState;
    }


















    public enum StateMachine 
    { 
        //巡逻
        Patrolling,

        //警惕
        Vigilant,

        //攻击
        Attack,

        //搜索
        Searching,
    }

}
