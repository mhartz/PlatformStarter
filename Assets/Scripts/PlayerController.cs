using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float MoveSpeed;
    public float JumpSpeed;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;

	void Start () {
        rb = GetComponent<Rigidbody2D>();

	}
	
	void Update () {
		MovePlayer();
        PlayerJump();
	}

    private void MovePlayer()
    {
        if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            rb.velocity = new Vector3(MoveSpeed, rb.velocity.y, 0f);
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            rb.velocity = new Vector3(-MoveSpeed, rb.velocity.y, 0f);
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        }
    }

    private void PlayerJump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        Debug.Log("isGrounded: " + isGrounded);
        Debug.Log("groundCheck.position: " + groundCheck.position);
        Debug.Log("groundCheckRadius: " + groundCheckRadius);
        Debug.Log("groundLayer: " + groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, JumpSpeed, 0f);
        }
    }
}
