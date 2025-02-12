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
    [SerializeField] private int maxJump = 2;
    [SerializeField] private float _yValJumpReleaseMod = 2f;
    private int _jumpLeft;
    // Start is called before the first frame update
    void Start()
    {
        _rigibody = GetComponent<Rigidbody2D>();
        _jumpLeft = maxJump;
    }

    // Update is called once per frame
    void Update()
    {
        var inputX = Input.GetAxisRaw("Horizontal");
        var jumpInput = Input.GetButtonDown("Jump");
        var jumpInputReleased = Input.GetButtonUp("Jump");
        _rigibody.velocity = new Vector2(inputX * speed, _rigibody.velocity.y);

        if(IsGround() && _rigibody.velocity .y <= 0)
        {
            _jumpLeft = maxJump;
        }

        if(jumpInput && _jumpLeft > 0)
        {
            _rigibody.velocity = new Vector2(_rigibody.velocity.x, jumpForce);
            _jumpLeft -= 1;
        }

        if (jumpInputReleased && _rigibody.velocity.y > 0 && _jumpLeft == 1)
        {
            _rigibody.velocity = new Vector2(_rigibody.velocity.x, _rigibody.velocity.y/_yValJumpReleaseMod);
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
