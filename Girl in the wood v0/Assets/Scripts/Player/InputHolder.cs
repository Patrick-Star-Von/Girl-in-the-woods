using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHolder : MonoBehaviour
{
    [HideInInspector]public Vector2 movement;

    InputActions inputActions;

    public void OnEnable()
    {
        if(inputActions == null){
            inputActions = new InputActions();
            inputActions.PlayerMove.Movement.performed += ctx => movement = ctx.ReadValue<Vector2>();
        }

        inputActions.Enable();
    }

}
