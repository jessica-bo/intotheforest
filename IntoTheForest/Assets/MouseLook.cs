using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Reference: https://www.youtube.com/watch?v=_QajrabyTJc

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // Prevents cursor clicking outside of screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Get inputs from the mouse and scale by sensitivity
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Use mouseY to control looking up/down with clamping
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Use mouseX to control Player body rotation
        playerBody.Rotate(Vector3.up * mouseX);

    }
}
