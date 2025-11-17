using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    //public CharacterController controller;
    public Rigidbody rb;
    public Transform Camera;
    //need to make separate control actions for each thing, like playerattack, playerinteract, etc 
    public PlayerInputActions playerControls;

    Vector3 direction = Vector3.zero;

    private InputAction move;
    private InputAction jump; 

   
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        //playerControls.Enable();
        move = playerControls.Player.Move;
        move.Enable();

       // jump = playerControls.Player.Jump;
    }

    private void OnDisable()
    {
        move.Disable();
    }

    private void Update()
    {
        //direction = move.ReadValue<Vector3>();
        Vector2 input = move.ReadValue<Vector2>();
        direction = new Vector3(input.x, 0, input.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        //reads in values from controller 
       // Vector3 direction = playerControls.ReadValue<Vector3>().normalized;
       
        //checking if we're getting input to move 
        if(direction.magnitude >= 0.1f)
        {
            //rotates the players body 
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            //move in the direction player is trying to move 
            //controller.Move(moveDir.normalized * speed * Time.deltaTime);

            Vector3 velocity = moveDir.normalized * speed;
            
         
            velocity.y = rb.linearVelocity.y;

            rb.linearVelocity = velocity;
           
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {

    }
}
