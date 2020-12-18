using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //public GameObject oldCam, newCam;
    public Camera oldCam, newCam;
    public static CameraManager instance;

    // Start is called before the first frame update

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        oldCam = newCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(newCam != oldCam)
        {
            //print("切换"+newCam.ToString());
            newCam.enabled = true;
            oldCam.enabled = false;
            oldCam = newCam;
        }
    }
}
