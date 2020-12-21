using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBall : MonoBehaviour
{
    public GameObject iceBallEffect;
    private void Start()
    {
        GetComponent<Rigidbody>().AddForce( transform.forward*5 + transform.up*10,ForceMode.Impulse);
    }
    private void OnDestroy()
    {
        Instantiate(iceBallEffect,transform.position,transform.rotation);
    }
}
