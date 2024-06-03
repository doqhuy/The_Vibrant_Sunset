using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private bool grounded;
    private bool impacted;
    public float moveSpeed = 10;
    public float jumpHeight = 10;
    public LayerMask groundLayer;
    public float groundCheckRadius = 1.5f;
    public Transform PlayerFeet;
    private AudioManager AudioManager;

    public MobileController MC;

    public LevelSave LS;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        GameObject AudioGO = GameObject.Find("AudioManager");
        AudioManager = AudioGO.GetComponent<AudioManager>();
        PlayerCombat PC = transform.GetComponent<PlayerCombat>();
        LS.SavePlayerCombat(PC.Inventory);
        int Level = LS.LoadLevel().Level;
        int MapNow = 0;
        if (SceneManager.GetActiveScene().name == "Scene1x")
        {
            MapNow = 1;
        }
        if (SceneManager.GetActiveScene().name == "Scene2")
        {
            MapNow = 2;
        }
        if (SceneManager.GetActiveScene().name == "Scene3")
        {
            MapNow = 3;
        }
        if (SceneManager.GetActiveScene().name == "Scene4")
        {
            MapNow = 4;
        }
        if (MapNow > Level)
        {
            LS.SaveLevel(MapNow);
        }

        if (LS.LoadPLayerCombat() != null)
        {
            PC.Inventory = LS.LoadPLayerCombat();
        }

        
    }

    

    void Update()
    {
        
            grounded = Physics2D.OverlapCircle(PlayerFeet.position, groundCheckRadius, groundLayer);
            PlayerCombat PC = transform.GetComponent<PlayerCombat>();
            if (PC.Stat.HP <= 0)
            {
                StartCoroutine(PC.DeadAction());
            }
            if (!PC.IsDameTaking && PC.Stat.HP > 0)
            {
                if ((Input.GetKeyDown(KeyCode.J) || MC.PressJ) && PC.CanHit)
                {
                    MC.SetPressJ(false);
                    AudioManager.HitSound();
                    animator.Play("Attack", 0, 0f);
                    PC.HitObjectCast();
                    PC.Stat.MP += 5;
                }
                if (Input.GetKey(KeyCode.K) || MC.HoldK)
                {
                    animator.Play("Protect", 0, 0f);
                    PC.IsProtect = Input.GetKey(KeyCode.K);
                }
                if ((Input.GetKeyDown(KeyCode.L) || MC.PressL) && PC.CanMissile)
                {
                    MC.SetPressL(false);
                    AudioManager.ShootSound();
                    animator.Play("Shoot", 0, 0f);
                    if (PC.Ammo > 0)
                    {
                        PC.Ammo--;
                        StartCoroutine(PC.MissileObjectCast());
                    }
                }
                if ((Input.GetKeyDown(KeyCode.U) || MC.PressU) && PC.CanSpell2 && PC.IsSpell2Equip)
                {
                    MC.SetPressU(false);
                    AudioManager.BurstSound();
                    PC.Stat.MP += 20;
                    animator.Play("Cast1", 0, 0f);
                    StartCoroutine(PC.BurstObjectCast());
                }
                if ((Input.GetKeyDown(KeyCode.I) || MC.PressI) && PC.CanSpell1 && PC.IsSpell1Equip)
                {
                    MC.SetPressI(false);
                    PC.Stat.MP += 20;
                    AudioManager.ShootSound();
                    animator.Play("Cast2", 0, 0f);
                    StartCoroutine(PC.ShootObjectCast());
                }
                JumpAction();
                if (grounded || (!impacted && !grounded)) MoveAction();
            }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            impacted = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            impacted = false;
        }
    }


    void JumpAction()
    {
        if ((Input.GetKey(KeyCode.Space) || MC.HoldSpace) && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            animator.SetTrigger("Jump");
        }
        //      if(rb.velocity.y < 0 && rb.velocity.y < -0.000001f) animator.SetBool("isFalling", true);
        //else if (rb.velocity.y >= -0.000001f && rb.velocity.y <= 0.000001f) animator.SetBool("isFalling", false);
        
        if (!grounded && rb.velocity.y <= 0 ) animator.SetBool("isFalling", true);
        else animator.SetBool("isFalling", false);
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            if(grounded)
            {
                animator.Play("Landing");
            }
        }    
    }

    void MoveAction()
    {
        float moveInput = Input.GetAxis("Horizontal");
        Vector3 scale = transform.localScale;
        if ((moveInput < 0 || MC.HoldA) && scale.x > 0)
        {
            scale.x *= -1;
            transform.localScale = scale;
        }
        else if ((moveInput > 0 || MC.HoldD) && scale.x < 0)
        {
            scale.x *= -1;
            transform.localScale = scale;
        }
        if (moveInput != 0f || MC.HoldD || MC.HoldA)
        {
            animator.SetBool("isMoving", true);
        }
        else if (grounded)
        {
            if (rb.velocity.y != 0f)
            {
            }
            else if (rb.velocity.y == 0f)
            {
            }
            animator.SetBool("isMoving", false);
        }
        if(MC.gameObject.activeSelf)
        {
            if(MC.HoldA)
            {
                rb.velocity = new Vector2(-1 * moveSpeed, rb.velocity.y);
            }
            if(MC.HoldD)
            {
                rb.velocity = new Vector2(1 * moveSpeed, rb.velocity.y);
            }
        }    
        else
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }
    }
}
