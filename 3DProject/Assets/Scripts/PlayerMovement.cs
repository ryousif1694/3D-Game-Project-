using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public CharacterController controller;
    public Transform Camera;
    //need to make separate control actions for each thing, like playerattack, playerinteract, etc 
    public InputAction playerControls;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        //float horizontal = Input.GetAxisRaw("Horizontal");
        //float vertical = Input.GetAxisRaw("Vertical");

        //Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        ////getting input to move
        //if(direction.magnitude >= 0.1f)
        //{
        //    controller.Move(direction * speed * Time.deltaTime);
        //}

        //reads in values from controller 
        Vector3 direction = playerControls.ReadValue<Vector3>().normalized;

        //checking if we're getting input to move 
        if(direction.magnitude >= 0.1f)
        {
            //rotates the players body 
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            //move in the direction player is trying to move 
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        
    }
}
