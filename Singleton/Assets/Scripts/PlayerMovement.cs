using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform cam;
    float vMouse;
    float hMouse;
    float yReal = 0.0f;
    public float horizontalSpeed;
    public float verticalSpeed;

    public CharacterController controller;
    public float speed = 12f;
    float x;
    float z;
    Vector3 move;
    Vector3 velocity;
    public float gravity = -15f;
    bool isGrounded = false;

    public float jumpForce = 1f;
    float jumpValue;

    // Start is called before the first frame update
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        jumpValue = Mathf.Sqrt(jumpForce * -2 * gravity);
    }

    // Update is called once per frame
    void Update()
    {
        LookMouse();
        if(isGrounded == true && velocity.y <0)
        {
            velocity.y = gravity;
        }
        Movement();
        Jump();
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void LookMouse()
    {
        hMouse = Input.GetAxis("Mouse X") * horizontalSpeed * Time.deltaTime;
        vMouse = Input.GetAxis("Mouse Y") * verticalSpeed * Time.deltaTime;

        yReal -= vMouse;
        yReal = Mathf.Clamp(yReal, -90f, 90f);
        transform.Rotate(0f, hMouse, 0f);
        cam.localRotation = Quaternion.Euler(yReal, 0f, 0f);
    }

    void Movement()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false;
            velocity.y = jumpValue;
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.collider.CompareTag("floor"))
        {
            if(isGrounded == false)
            {
                isGrounded = true;
            }
        }
    }


}
