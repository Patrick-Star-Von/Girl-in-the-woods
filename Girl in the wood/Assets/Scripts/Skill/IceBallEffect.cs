using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBallEffect : MonoBehaviour
{
    EnemyAI enemy = null;
    //Queue<>
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Enemy")
        {
            enemy = other.GetComponent<EnemyAI>();
            enemy.nav.speed = 0f;
            enemy.alertValue = 0f;
        }
    }

    private void OnDestroy()
    {
        if(enemy != null)
        {
            enemy.nav.speed = 2.5f;
        }
   
    }
}
