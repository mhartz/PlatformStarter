using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float MoveSpeed;
    public float JumpSpeed;
    public bool AllowDoubleJump;

    private Rigidbody2D rb;
    private bool IsGrounded;
    private int JumpCount = 0;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		MovePlayer();

	    if (AllowDoubleJump)
	    {
	        PlayerDoubleJump();
	    }
	    else
	    {
	        PlayerJump();
        }
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
        if (Input.GetButtonDown("Jump") && IsGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, JumpSpeed, 0f);
        }
    }

    private void PlayerDoubleJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (JumpCount < 2)
            {
                rb.velocity = new Vector3(rb.velocity.x, JumpSpeed, 0f);
                JumpCount++;
            }                
        }
    }

    private void Instakill()
    {
        //Todo : Need to implement a respawn based on the Game Manager

        transform.position = new Vector3(0, 0, 0);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            IsGrounded = true;

            if (AllowDoubleJump)
                JumpCount = 0;
        }

        if (collision.gameObject.tag == "Instakill")
            Instakill();
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            IsGrounded = false;
        }
    }
}
