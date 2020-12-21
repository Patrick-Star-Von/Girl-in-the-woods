using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        int skillSelect = MouseManager.ins.skillSelect;
        if (skillSelect == 0)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else if(skillSelect == 1)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
        else if(skillSelect == 2)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = Color.black;
        }
        
    }
}
