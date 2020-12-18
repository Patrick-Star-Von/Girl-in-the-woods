using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    int count = 15;
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "sword")
        {
            print("啊啊啊啊啊啊啊，嗯嗯嗯啊啊啊啊啊啊去");
            if (--count == 0)
                Destroy(this.gameObject);
        }
    }
}
