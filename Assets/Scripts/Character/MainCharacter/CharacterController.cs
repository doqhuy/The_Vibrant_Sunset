using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Netcode;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    public float moveSpeed = 80f;
    public float jumpHeight = 100f;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        this.tag = "Player";
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            animator.Play("Attack", 0, 0f);
        }
        if (Input.GetKey(KeyCode.K))
        {
            animator.Play("Protect", 0, 0f);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            animator.Play("Shoot", 0, 0f);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            animator.Play("Cast1", 0, 0f);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            animator.Play("Cast2", 0, 0f);
        }
        JumpAction();
        MoveAction();
    }

    void JumpAction()
    {
        if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.y == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            animator.SetTrigger("Jump");
        }
        if(rb.velocity.y < 0 && rb.velocity.y < -0.000001f) animator.SetBool("isFalling", true);
		else if (rb.velocity.y >= -0.000001f && rb.velocity.y <= 0.000001f) animator.SetBool("isFalling", false);
	}

    void MoveAction()
    {
         float moveInput = Input.GetAxis("Horizontal");
        Vector3 scale = transform.localScale;
        if (moveInput < 0 && scale.x > 0)
        {
            scale.x *= -1;
            transform.localScale = scale;
        }
        else if (moveInput > 0 && scale.x < 0)
        {
            scale.x *= -1;
            transform.localScale = scale;
        }
        if (moveInput != 0f)
        {
            //animator.SetFloat("Action", 1);
            animator.SetBool("isMoving", true);
        }
        else if (Math.Abs(moveInput) <= 0.3f)
        {
            if (rb.velocity.y != 0f)
            {
                //animator.SetFloat("Action", 1);
            }
            else if (rb.velocity.y == 0f)
            {
                //animator.SetFloat("Action", 0);
            }
            animator.SetBool("isMoving", false);
        }
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }
}
