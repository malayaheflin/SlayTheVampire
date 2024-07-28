using UnityEngine;

public class movement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    // Update is called once per frame
    void Update()
    {
        var xDir = Input.GetAxis("Horizontal");
        var zDir = Input.GetAxis("Vertical");

        Vector3 move = transform.right * xDir  + transform.forward * zDir;

        controller.Move(move * speed * Time.deltaTime);
    }

}
