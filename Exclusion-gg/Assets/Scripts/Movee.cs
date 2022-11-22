using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movee : MonoBehaviour
{

    Rigidbody rb;
    public float speed = 12f;
    public float sprintSpeed = 20f;
    public float jumpHeight = 3f;
    public float regularSpeed = 12f;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public CharacterController controller;
    Vector3 velocity;
    bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetKey(KeyCode.LeftControl)) 
        {
            speed = sprintSpeed;
        } else
        {
            speed = regularSpeed;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded )
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
