using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    public float speed = 8f;
    public float gravity = -9.8f;
    public float rotationSpeed = 8f;

    Transform mainCam;
    InputHolder inputHolder;
    new Rigidbody rigidbody;
    float vertical;
    float horizontal; 
    bool grounded;
    bool falling;

    Vector3 normalVector;
    Vector3 moveDirection;
    PlayerManager manager;

    void Start(){
        rigidbody = GetComponent<Rigidbody>();
        inputHolder = GetComponent<InputHolder>();
        mainCam = Camera.main.transform;
        manager = PlayerManager.S;
    }

    void Update(){
        vertical = inputHolder.movement.y;
        horizontal = inputHolder.movement.x;
        DetectFalling();
        DoMove();
    }

    void DoMove(){
        if(manager.isInAir)
            return;
        HandleMove();
        HandleRotate();
    }

    public void HandleMove(){
        moveDirection = new Vector3(horizontal, gravity, vertical);
        moveDirection.Normalize();

        Quaternion tmp = mainCam.rotation;
        mainCam.eulerAngles = new Vector3(0, mainCam.eulerAngles.y, 0);
        moveDirection = mainCam.TransformDirection(moveDirection);
        mainCam.rotation = tmp;

        moveDirection *= speed;
        moveDirection = Vector3.ClampMagnitude(moveDirection, speed);

        Vector3 projected = Vector3.ProjectOnPlane(moveDirection, normalVector);
        projected.y = 0;

        rigidbody.velocity = projected;
    }

    public void HandleRotate(){
        Vector3 targetRotation = Vector3.zero;
        targetRotation = moveDirection;

        targetRotation.Normalize();
        targetRotation.y = 0;

        if(targetRotation == Vector3.zero){
            targetRotation = transform.forward;
        }

        Quaternion rotation = Quaternion.LookRotation(targetRotation);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    public void DetectFalling(){
        RaycastHit hit;

        if(Physics.Raycast(transform.position, -transform.up, out hit, .3f)){
            normalVector = hit.normal;
        }
    }
}
