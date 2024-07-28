using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity = 4f;
    float cameraVertRotation = 0f;

    void Start()
    {
        Cursor.visible=false;
        Cursor.lockState=CursorLockMode.Locked;
    }
    void Update()
    {
        float inputX = Input.GetAxis("Mouse X")*mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y")*mouseSensitivity;

        //looking up and down
        cameraVertRotation -= inputY;
        cameraVertRotation = Mathf.Clamp(cameraVertRotation, -90f, 90f);
        transform.localRotation=Quaternion.Euler(cameraVertRotation, 0f, 0f);

        //rotating left and right
        player.Rotate(Vector3.up * inputX);

    }
}
