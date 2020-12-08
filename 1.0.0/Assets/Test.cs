using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        
    }


    private void Update()
    {
        print(transform.localEulerAngles);
        //transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles);
    }

}
