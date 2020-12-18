using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySight : MonoBehaviour
{
    public float viewAngle = 110;
    public bool playerInSight;
    public Vector3 personalLastSighting;

    private NavMeshAgent nav;
    private SphereCollider col;
    private Animator anim;
    private GameObject player;
    private Animator playerAnim;
    private bool isPlayerDead;

    void Start(){
        nav = GetComponent<NavMeshAgent>();
        col = GetComponent<SphereCollider>();
        player = PlayerManager.S.player;
    }

    void Update(){
        Vector3 direction = player.transform.position - this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.forward);
        
        if(angle < viewAngle * 0.5f){
            RaycastHit hit;

            if(Physics.Raycast(transform.position + transform.up * 2, direction.normalized, out hit, col.radius)){
                if(hit.collider.gameObject == player){
                    // Raise Awareness

                    // Perform action
                }
            }
        }
    }
}
