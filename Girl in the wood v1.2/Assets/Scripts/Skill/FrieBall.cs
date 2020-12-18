using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrieBall : MonoBehaviour
{
    float pTime;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject,5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * 0.2f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Small objects")
        {
            Destroy(other.gameObject);
        }
        else if(other.tag == "Enemy")
        {
            other.GetComponent<EnemyAI>().startUp_Skill_2(transform);
        }
        else if(other.tag == "iceBall")
        {
            other.GetComponent<Rigidbody>().AddForce(transform.forward * 10f, ForceMode.Impulse);
        }
        Destroy(this.gameObject);
    }
}
