using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Look related
    [SerializeField] float mouseSensitivity = 100f;
    [SerializeField] Transform camera;
    float xRotation = 0f;
    //Move related
    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 12f;
    //Gravity related
    [SerializeField] float gravity = -9.81f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;
    //Jump related
    Vector3 velocity;
    bool isGrounded;
    [SerializeField] float jumpHeigh = 3f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame   
    void Update()
    {
        ApplyGravity();
        Look();
        Move();
        Jump();
    }

    private void Jump()
    {
        if (Input.GetKeyDown("space") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeigh * -2f * gravity);
            Debug.Log("ups");
            controller.Move(velocity * Time.deltaTime);
        }
    }

    private void ApplyGravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y > 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    private void Move()
    {
        // Get the input values
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        //Apply that values
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
    }

    private void Look()
    {
        //Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Rotate in X and Limit the X rotation to 90
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Rotate in Y
        camera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }


}
