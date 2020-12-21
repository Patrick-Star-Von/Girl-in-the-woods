using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    // Start is called before the first frame update



    private void OnTriggerStay(Collider other)
    {
        if (!PlayerHave.ins.haveStone && other.tag == "Player" && Input.GetButtonDown("J_Key"))
        {
            Destroy(this.gameObject);
            PlayerHave.ins.haveStone = true;
        }

    }
}
