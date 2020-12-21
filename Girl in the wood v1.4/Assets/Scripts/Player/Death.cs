using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    public GameObject deadUI;
    int count = 15;
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "sword")
        {
            print("啊啊啊啊啊啊啊，嗯嗯嗯啊啊啊啊啊啊去");
            if (--count == 0)
                SceneManager.LoadScene("Death");
                

        }
    }
}
