using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform routes;//路径集合
    public Transform playerTransform;//玩家transform
    public float pauseTime = 3.0f;//巡逻暂停时长
    public float alertTime = 3.0f;//警戒时长
    public float detectRange = 10.0f;//检测范围
    public float detectAngle = 120.0f;//视角
    public GameObject sword;

    [HideInInspector]
    public bool IN_VIEW;//视野之内
    [HideInInspector]
    public bool IN_DISTANCE;//距离之内
    [HideInInspector]
    public bool SEE_ME;//看见我？
    [HideInInspector]
    public bool NO_WALL;//无墙阻隔
    [HideInInspector]
    public bool FIND_TARGET;//找到目标
    [HideInInspector]
    public Vector3 localEulerAngles;

    //组件变量
    [HideInInspector]
    public Light light;
    [HideInInspector]
    public NavMeshAgent nav;
    StateMachine state;

    //变量
    Coroutine FollowPlayer_ = null;
    Coroutine ToTurnTo_ = null;
    RaycastHit hit;
    Vector3 enemytoplayerVector;
    Vector3 eyePosition;
    Vector3 finPlayerPosition;


    List<Vector3> path;//存储所有巡逻点
    Vector3 point;//暂时存储单个巡逻点
    float time;//定时判断变量
    bool flag_arrive = true;//是否到达下一个点
    bool flag_time = true;//是否到时
    Vector3 oldPosition;
    int path_i = 0;//巡逻点的索引

    [HideInInspector]
    public float alertValue = 0f;





    bool inLoop = true;






    private void Awake()
    {
        light = GetComponentInChildren<Light>();
        nav = GetComponent<NavMeshAgent>();
        path = new List<Vector3>();
        state = StateMachine.Patrolling;
    }



    // Start is called before the first frame update
    void Start()
    {
        //print("routes.childCount:"+routes.childCount);
        for (int i = 0; i < routes.childCount; i++)
        {//获取路径集合
            //print(routes.GetChild(i).position);
            path.Add(routes.GetChild(i).position);
            //path[i].y = transform.position.y;
            //print(i);
        }
        print(sword.transform.localEulerAngles);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Vector3.Distance(playerTransform.position,transform.position) < 1.5f)
        {
            nav.SetDestination(playerTransform.position);
            StartCoroutine(Attack());
        }



        //Debug.DrawLine(finPlayerPosition, playerTransform.position);


        //眼睛的位置
        eyePosition = transform.GetChild(0).GetChild(0).position;
        //敌人到玩家的向量
        enemytoplayerVector = playerTransform.position - transform.position;

        IN_DISTANCE = Vector3.Distance(transform.position, playerTransform.position) < detectRange;
        IN_VIEW = Vector3.Angle(transform.forward, enemytoplayerVector) < detectAngle * 0.5f;
        NO_WALL = Physics.Raycast(eyePosition, playerTransform.position - eyePosition, out hit) && hit.transform.position == playerTransform.position;
        SEE_ME = IN_VIEW && NO_WALL && Vector3.Distance(transform.position, playerTransform.position) < detectRange * 2.5f;


        //--------------------------------------------------------枚举的状态机----------------------------------------------------------------------
        //switch (state)
        //{
        //    case StateMachine.Patrolling:
        //        IN_VIEW = Vector3.Angle(transform.forward, enemytoplayerVector) < detectAngle * 0.5f ? true : false;
        //        NO_WALL = Physics.Raycast(eyePosition, playerTransform.position - eyePosition, out hit) && hit.transform.position == playerTransform.position ? true : false;
        //        SEE_ME = IN_VIEW && NO_WALL && Vector3.Distance(transform.position, playerTransform.position) < detectRange * 2.5f ? true : false;
        //        if (SEE_ME)
        //        {
        //            state = StateMachine.Vigilant;
        //        }
        //        else
        //        {
        //            //state = StateMachine.Patrolling;
        //        }
        //        break;
        //    case StateMachine.Vigilant:
        //        alertValue += (detectRange * 2.5f - Vector3.Distance(playerTransform.position, transform.position)) * 0.05f;
        //        if(alertValue > 40f)
        //        {
        //            state = StateMachine.Near;
        //        }
        //        //if ()//丢失玩家
        //        //{
        //            //state = StateMachine.Search; 
        //        //}
        //        break;
        //    case StateMachine.Near:
        //        alertValue += (detectRange * 2.5f - Vector3.Distance(playerTransform.position, transform.position)) * 0.05f;
        //        if (alertValue > 90f)
        //        {
        //            state = StateMachine.Aggressive;
        //        }
        //        //if ()//丢失玩家
        //        //{
        //        //state = StateMachine.Search; 
        //        //}
        //        break;
        //    case StateMachine.Aggressive:
        //        //------------
        //        //执行攻击代码
        //        //------------
        //        //if ()//丢失玩家
        //        //{
        //        //state = StateMachine.Search; 
        //        //}
        //        break;

        //    case StateMachine.Search:
        //        alertValue -= 0.1f;
        //        //------------
        //        //执行搜寻代码
        //        //------------
        //        if (alertValue < 2f)
        //        {
        //            state = StateMachine.Patrolling;
        //        }
        //        break;
        //    default:

        //        break;
        //}
        //-----------------------------------------------------------------------------------------------------------------------------------------

        if (IN_DISTANCE && SEE_ME)//在检测范围内，能看见我
        {
            alertValue = 100f;
            FIND_TARGET = true;
        }
        else if (!IN_DISTANCE && SEE_ME)//在检测范围外，能看见我
        {
            //（警戒）
            FIND_TARGET = true;
            if (alertValue < 100f)
            {
                alertValue += (detectRange * 2.5f - Vector3.Distance(playerTransform.position, transform.position)) * 0.05f;
            }
        }
        else if (!SEE_ME && FIND_TARGET)//眼睛看不见，之前找到过目标
        {
            
            //（丢失目标）
            nav.speed = 2.5f;
            if(FollowPlayer_ == null)
            {
                FollowPlayer_ = StartCoroutine(FollowPlayer());
            }
        }
        else if (IN_DISTANCE && !IN_VIEW && NO_WALL)//眼睛看不见，但是在检测范围内
        {
            //（在背后）
            //print("我感觉背后有阵阵凉意");
        }
        else
        {
            if (alertValue > 0f)
            {
                if (ToTurnTo_ == null)
                {
                    //ToTurnTo_ = StartCoroutine(ToTurnTo(180));
                }
                alertValue -= 0.1f;
            }
        }


        if (alertValue > 90f)//快速走过来攻击
        {
            light.color = Color.red;
            nav.speed = 5.5f;
            nav.SetDestination(playerTransform.position);
        }
        else if(alertValue > 40f)//慢速走过来
        {
            light.color = Color.white;
            nav.speed = 2.5f;
            nav.SetDestination(finPlayerPosition);
        }
        else if (alertValue > 5f)
        {
            transform.LookAt(playerTransform.position);
            nav.speed = 0f;
            finPlayerPosition = playerTransform.position;
        }
        else if(alertValue > -5f)//重置巡逻
        {
            light.color = Color.white;
            flag_arrive = true;//设置为可巡逻状态
            flag_time = true;//设置为暂停时间未存储状态
            ToTurnTo_ = null;
            alertValue = -10f;
        }
        else
        {
            light.color = Color.white;
            
            if (flag_arrive)
            {
                //print("开始巡逻");
                point = path[path_i % path.Count];
                nav.SetDestination(point);//走到第i个巡逻点
                if (!nav.hasPath)
                {
                    //print("find route");
                    path_i++;
                    flag_arrive = false;//设置为不可巡逻状态
                }
            }
            else if (Vector3.Distance(transform.position, point) < 2.0f)
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

            //if (oldPosition != transform.position)
            //{
            //    oldPosition = transform.position;
            //    countTime = 0;
            //}
            //else if (countTime > 2f)
            //{
            //    print("重置");
            //    nav.enabled = false;
            //    nav.enabled = true;
            //    nav.speed = 2.5f;
            //    countTime = 0f;
            //}
            //else
            //{
            //    countTime += Time.deltaTime;
            //}


        }
    }




    public IEnumerator FollowPlayer()
    {
        float time = Time.time + 2.5f;
        while(Time.time < time)
        {
            nav.SetDestination(playerTransform.position);
            finPlayerPosition = playerTransform.position;
            yield return 0;
        }
        FIND_TARGET = false;
        FollowPlayer_ = null;
        yield break;
    }




    IEnumerator ToTurnTo(float angle, float limAngle = 5f)
    {//参数：角度，转速
        float i = angle / limAngle;
        while (i-- > 0)
        {
            print(i);
            transform.Rotate(Vector3.up, limAngle);
            yield return null;
        }
        yield break;
    }




    //IEnumerator MoveTo(Vector3 target, float speed = 3.5f)
    //{//参数：坐标，速度
    //    bool flag = true;
    //    nav.speed = speed;
    //    moveto_ = false;
    //    while (Vector3.Distance(transform.position, target) > 3f)
    //    {
    //        if (flag)
    //        {
    //            nav.SetDestination(target);
    //            flag = false;
    //        }
    //        yield return 0;
    //    }
    //    moveto_ = true;
    //    yield break;
    //}


    public enum StateMachine
    {
        Patrolling,
        Vigilant,
        Near,
        Search,
        Aggressive,
    }

    public IEnumerator Skill_1(float t = 3f)
    {
        float pTime = Time.time + t;
        
        while(Time.time < pTime)
        {
            //print("alertValue is" + alertValue);
            nav.speed = 0.5f;
            yield return 0;
        }
        nav.speed = 2.5f;
        yield break;
    }

    //public void Skill_2(Transform fireTransform)
    //{
    //    transform.position += transform.forward * -10f;
    //}

    public IEnumerator Skill_2(Transform fireTransform)
    {
        Vector3 attackVector = transform.position - fireTransform.position;
        Vector3 position = fireTransform.position;
        attackVector.Normalize();
        float pTime = Time.time + 0.15f;
        while(Time.time < pTime)
        {
            //print("attack");
            transform.position += attackVector * 0.25f;
            yield return 0;
        }
        alertValue += 60f;
        yield break;
    }

    public IEnumerator Attack()
    {
        float pTime = 0;
        localEulerAngles = sword.transform.localEulerAngles;
        while (pTime < 0.5f)
        {
            pTime += Time.deltaTime;
            sword.transform.localEulerAngles = Vector3.Lerp(sword.transform.localEulerAngles, new Vector3(180, 0f, 0f), Time.deltaTime * 2);
            yield return 0;
        }

        StartCoroutine(Attack2());
        yield break;
    }

    public IEnumerator Attack2()
    {
        float pTime = 0;
        while (pTime < 0.5f)
        {
            pTime += Time.deltaTime;
            sword.transform.localEulerAngles = Vector3.Lerp(sword.transform.localEulerAngles, new Vector3(0f, 0f, 90f), Time.deltaTime * 2);
            yield return 0;
        }
        sword.transform.localEulerAngles = localEulerAngles;
        yield break;
    }

    public void startUp_Skill_2(Transform fireTransform)
    {
        EnemyAI enemy = GetComponent<EnemyAI>();
        if (!enemy.enabled)
        {
            enemy.enabled = true;
        } 
        StartCoroutine(Skill_2(fireTransform));
    }

}
