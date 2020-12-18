using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitch : MonoBehaviour
{
    CameraManager cameraManager;
    // Start is called before the first frame update
    void Start()
    {
        cameraManager = CameraManager.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            cameraManager.newCam = GetComponentInParent<Camera>(); 
            //print("进入摄像机"+this.name);
        }
    }
}
