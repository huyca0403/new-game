using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigibody;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.05f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask collisionMask;

    // Start is called before the first frame update
    void Start()
    {
        _rigibody = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        var inputX = Input.GetAxisRaw("Horizontal");
        var jumpInput = Input.GetButtonDown("Jump");

        _rigibody.velocity = new Vector2(inputX * speed, _rigibody.velocity.y);

        if(jumpInput && IsGround())
        {
            _rigibody.velocity = new Vector2(_rigibody.velocity.x, jumpForce);
        }

        if(inputX != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(inputX), 1, 1);
        }
    }
    private bool IsGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius,collisionMask);
    }
}
