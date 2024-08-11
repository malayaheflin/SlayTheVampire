using UnityEngine;

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
    CharacterController controller;
    bool isGrounded;

    Vector3 velocity;

    void Awake()
    {
        controller = GetComponent<CharacterController>();

        gravity = -30f;

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

        if(Input.GetKeyDown(jumpKey))
            Jump();
    }

    void Jump()
    {
        if (isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
    }
}
