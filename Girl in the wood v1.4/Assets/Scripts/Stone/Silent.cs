using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Silent : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            print("进入禁魔石范围");
            other.GetComponent<Skill>().silent = true;      
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            print("可以使用技能了");
            other.GetComponent<Skill>().silent = false;
        }
    }
}
