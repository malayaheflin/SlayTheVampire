using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterControl : MonoBehaviour
{
    // eventually add animators & sound here too
    public float speed = 12f;
    public float gravity = -30f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float jumpHeight = 1.2f;
    public KeyCode jumpKey = KeyCode.Space;
    public float dashDistance = 5f;
    CharacterController controller;
    bool isGrounded;
    private bool isDashing = false;
    private float dashingPower = 20f;
    private float dashingDuration= 0.3f;
    private float dashingCooldown = 0f;


    Vector3 velocity;

    void Awake()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //Checking if grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }

        //Dashing
        if(Input.GetKey(KeyCode.W) && !isDashing)
        {
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                Debug.Log("Dash Forward");
                StartCoroutine(Dash(transform.forward, Vector3.zero));
            }
        }

        if(Input.GetKey(KeyCode.A) && !isDashing)
        {
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                Debug.Log("Dash Left");
                StartCoroutine(Dash(Vector3.zero, transform.right * -1));
            }
        }

        if(Input.GetKey(KeyCode.S) && !isDashing)
        {
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                Debug.Log("Dash Back");
                StartCoroutine(Dash(transform.forward * -1, Vector3.zero));
            }
        }

        if(Input.GetKey(KeyCode.D) && !isDashing)
        {
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                Debug.Log("Dash Right");
                StartCoroutine(Dash(Vector3.zero, transform.right));
            }
        }

        //Moving
        var xDir = Input.GetAxis("Horizontal");
        var zDir = Input.GetAxis("Vertical");

        Vector3 move = transform.right * xDir  + transform.forward * zDir;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        //Jumping
        if(Input.GetKeyDown(jumpKey))
            Jump();
    }

    void Jump()
    {
        if (isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
    }

    private IEnumerator Dash(Vector3 forward, Vector3 right)
    {
        isDashing = true;
        velocity = forward * dashingPower + right * dashingPower;
        yield return new WaitForSeconds(dashingDuration);
        velocity = Vector3.zero;
        yield return new WaitForSeconds(dashingCooldown);
        isDashing = false;
    }
}
