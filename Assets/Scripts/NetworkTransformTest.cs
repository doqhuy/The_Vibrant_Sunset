using System.Collections;
using System.Collections.Generic;
using System;
using Unity.Netcode;
using UnityEngine;

public class NetworkTransformTest : NetworkBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    public float moveSpeed = 5f;
    public float jumpHeight = 10f;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (!IsOwner)
        {
            //float theta = Time.frameCount / 10.0f;
            //transform.position = new Vector3((float)Math.Cos(theta), 0.0f, (float)Math.Sin(theta));
            if (Input.GetKey(KeyCode.J))
            {
                animator.Play("Attack", 0, 0f);
            }
            JumpAction();
            MoveAction();
        }
    }

    void JumpAction()
    {
        if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.y == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
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
            animator.SetFloat("Action", 1);
        }
        else
        {
            if (rb.velocity.y != 0f)
            {
                animator.SetFloat("Action", 1);
            }
            else if (rb.velocity.y == 0f)
            {
                animator.SetFloat("Action", 0);
            }
        }
        //transform.position = new Vector2(moveInput * moveSpeed, transform.position.y);
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }
}