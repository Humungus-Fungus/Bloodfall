using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 12f;
    public float jumpHeight = 10f;
    public CharacterController _cc;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;


    Transform _transform;
    Vector3 velocity;
    bool isGrounded;

    void Start()
    {
        _transform = transform;
        _cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Raycast to the ground (we can have two to represent the feet of the character)
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, groundDistance, groundMask);
        // Debug.DrawRay(groundCheck.position, Vector3.down, Color.yellow, groundDistance);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        OnDirectionalMovement();
        Jump();

        velocity.y += gravity * Time.deltaTime;
        _cc.Move(velocity * Time.deltaTime);
        
    }

    void OnDirectionalMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // if both values are zero
        if (x == 0 && z == 0) return;

        Vector3 motion = _transform.right * x + _transform.forward * z;
        _cc.Move( motion * speed * Time.deltaTime );
    }

    void Jump()
    {
        if (Input.GetAxis("Jump") == 0 || !isGrounded) return;
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
}
