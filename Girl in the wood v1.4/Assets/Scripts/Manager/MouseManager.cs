using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    [HideInInspector]
    public static MouseManager ins;
    // Start is called before the first frame update
    public Transform player;
    [HideInInspector]
    public int skillSelect;
    [HideInInspector]
    public bool aiming = false;

    public float 鼠标滚轮切换技能速度 = 4f;

    float scrollWheel = 10000f;
    void Start()
    {
        if(ins == null)
        {
            ins = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 鼠标滚轮切换技能速度;


        if (Input.GetMouseButton(1))
        {
            player.LookAt(Vector3.Lerp(player.position + player.forward,GetPointForScreen(),0.1f));
            aiming = true;
        }
        else
        {
            aiming = false;
        }
        scrollWheel += Input.GetAxis("Mouse ScrollWheel") * speed;
        skillSelect = (int)scrollWheel % 4;
        
    }


    public Vector3 GetPointForScreen()
    {
        RaycastHit ScreenToWorld_hit;
        Ray ray = CameraManager.instance.newCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out ScreenToWorld_hit))
        {
            return ScreenToWorld_hit.point;
        }
        return Vector3.zero;
    }


}
