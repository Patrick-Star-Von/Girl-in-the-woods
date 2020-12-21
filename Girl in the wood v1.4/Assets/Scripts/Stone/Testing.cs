using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    Light light;
    float r;
    private void Start()
    {
        light = GetComponent<Light>();
        r = GetComponent<SphereCollider>().radius;
        //intensity = this.GetComponent<Light>().intensity;
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            light.intensity = (1 - Vector3.Distance(other.transform.position, transform.position) / r) * 10f;
        }
    }
}
