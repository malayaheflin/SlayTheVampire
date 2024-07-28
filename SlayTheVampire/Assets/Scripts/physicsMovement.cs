using UnityEngine;

public class physicsMovement : MonoBehaviour
{
    public float speed = 12f;
    public float speedMultiplier = 10f;
    float horizontalMovement;
    float verticalMovement;
    Rigidbody rb;
    Vector3 moveDirection;
    float rbDrag = 6f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        MyInput();
        ControlDrag();
    }

    private void ControlDrag()
    {
        rb.drag = rbDrag;
    }

    private void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        rb.AddForce(moveDirection.normalized * speed * speedMultiplier, ForceMode.Acceleration);
    }
}
