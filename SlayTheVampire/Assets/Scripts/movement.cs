using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class movement : MonoBehaviour
{
    // eventually add animators & sound here too

    public float speed = 12f;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float jumpHeight = 1.2f;

    PlayerInput playerInput;
    PlayerInput.MainActions input;

    CharacterController controller;
    bool isGrounded;


    Vector3 velocity;

    void Awake()
    {
        controller = GetComponent<CharacterController>();

        playerInput = new PlayerInput();
        input = playerInput.Main;
        AssignInputs();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }

        var xDir = Input.GetAxis("Horizontal");
        var zDir = Input.GetAxis("Vertical");

        Vector3 move = transform.right * xDir  + transform.forward * zDir;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    void OnEnable()
    {
        input.Enable();
    }
    void OnDisable()
    {
        input.Disable();
    }

    // for action mapping (input system package)
    void AssignInputs()
    {
        input.Jump.performed += ctx => Jump();
    }

    void Jump()
    {
        if (isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
    }

}
