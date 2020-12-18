using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBall : MonoBehaviour
{
    public GameObject iceBallEffect;
    private void OnDestroy()
    {
        Instantiate(iceBallEffect,transform.position,transform.rotation);
    }
}
