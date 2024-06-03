using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator animator;
    protected Rigidbody2D rb;
    protected CapsuleCollider2D capsule;
    [SerializeField]
    protected float moveSpeedOrigin = 2f;
    private float moveSpeed = 2f;
    [SerializeField]
    protected float jumpHeight = 130f;
    [SerializeField]
    protected int Direction = 1;
    [SerializeField]
    protected float distanceToEdge = 2.0f;
    [SerializeField]
    protected float distanceToWall = 2.0f;
	[SerializeField]
	protected float distanceToPatrolLimit = 2.0f;
	[SerializeField]
    protected float detectDistance = 5.0f;
    public float NearDistanceX = 3f;
    public float NearDistanceY = 3f;
    [SerializeField]
    protected float TriggeredAccelerate = 5f;
    protected bool isDelayingForAction = false;
    protected bool isTriggering = false;
    // Start is called before the first frame update
    public bool isAttacking = false;
    private bool isDameTaking = false;
    public enum EnemyType { Ranged, Melee}
    public EnemyType Type;

    private GameObject Player;
 

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        capsule = GetComponent<CapsuleCollider2D>();
		gameObject.layer = LayerMask.NameToLayer("Enemy");
        Player = GameObject.Find("Player");
	}

    // Update is called once per frame
    void Update()
    {
        EnemyCombat EC = transform.GetComponent<EnemyCombat>();
        

        if (EC.Stat.HP <= 0)
        {
            StartCoroutine(DeadAction());
        }
        else if(!isDameTaking) 
        {
            if (isNearPlayer())
            {
                animator.SetBool("isMoving", false);
                if (EC.IsCoolDown == false)
                {
                    if(Type == EnemyType.Ranged)
                    {
                        animator.Play("Attack");
                        StartCoroutine(EC.ShootAttack());
                    }
                    else
                    {
                        animator.Play("Attack");
                        StartCoroutine(EC.HitAttack());
                    }
                    
                }
            }
            MoveAction();
        }


        if (PlayerDectected())
        {
            if (!(moveSpeed >= moveSpeedOrigin + TriggeredAccelerate))
            {
                moveSpeed += TriggeredAccelerate;
            }
        }
        else
        {
            moveSpeed = moveSpeedOrigin;
        }

        
        

    }

    public List<GameObject> DropObjectWhenDie;

    IEnumerator DeadAction()
    {
        animator.Play("Dead");
        yield return new WaitForSeconds(2);
        foreach (GameObject DO in DropObjectWhenDie)
        {
            if(DO != null)
            {
                Instantiate(DO, transform.position, Quaternion.identity);
            }
        }
        Destroy(this.gameObject);
    }

    protected void MoveAction()
    {
        if (isNearPlayer())
        {
            if(Player.transform.position.x < transform.position.x)
            {
                Direction = -1;
            }
            else
            {
                Direction = 1;
            }
            ChangeDirection();
            //if (!PlayerDectected())
            //{
            //    Direction *= -1;
            //    ChangeDirection();
            //}
            rb.velocity = new Vector2((float)0 * Direction, rb.velocity.y);
        }
        else if (!PlayerDectected() && (isNearlyOffEdge() || isNearWall() || isNearPatrolLimit()))
        {
            if (!isDelayingForAction)
            {
                isDelayingForAction = true;
                animator.SetBool("isMoving", false);
                StartCoroutine(DelayBeforeAction());
            }
        }
        else
        {
            rb.velocity = new Vector2((float)moveSpeed * Direction, rb.velocity.y);
            if (Math.Abs(rb.velocity.x) <= 0.3f || isNearPlayer())
            {
                animator.SetBool("isMoving", false);
            }
            else if (rb.velocity.x != 0f)
            {
                animator.SetBool("isMoving", true);
            }
        }
    }

    protected bool PlayerDectected()
    {
		RaycastHit2D result = Physics2D.Raycast(transform.position, new Vector2(Direction, 0), detectDistance);
        if(result.collider != null)
        {
            if (result.collider.CompareTag("Player")) return true;
        }
		return false;
	}

    protected void ChangeDirection()
    {
        Vector3 scale = transform.localScale;
        if(Direction > 0)
        {
            scale.x = 1;
            transform.localScale = scale;
		}
        else
        {
			scale.x = -1;
			transform.localScale = scale;
		}
    }

    protected bool isNearlyOffEdge()
    {
        RaycastHit2D result = Physics2D.Raycast(new Vector3(transform.position.x + (distanceToEdge * Direction), transform.position.y, transform.position.z), Vector2.down, capsule.size.y / 2 + 0.05f);
        if(result.collider != null)
        {
            return false;
        }
        return true;
    }

    protected bool isNearWall()
    {
		RaycastHit2D result = Physics2D.Raycast(transform.position, new Vector2(Direction, 0), distanceToWall);
        if (result.collider != null)
        {
            if (result.collider.CompareTag("Obstacle")) return true;
		}
		return false;
	}

    protected bool isNearPatrolLimit()
    {
		RaycastHit2D result = Physics2D.Raycast(transform.position, new Vector2(Direction, 0), distanceToPatrolLimit);
		if (result.collider != null)
		{
            if (result.collider.CompareTag("PatrolLimit")) return true;
		}
		return false;
	}

    protected IEnumerator DelayBeforeAction()
    {
        yield return new WaitForSeconds(2.0f);
        Direction *= -1;
        isDelayingForAction = false;
		animator.SetBool("isMoving", true);
		ChangeDirection();
        MoveAction();
    }

    private bool isNearPlayer()
    {
        if(Vector2.Distance(Player.transform.position, transform.position) <= 10f)
        {
            if(Mathf.Abs(Player.transform.position.y - transform.position.y) <= NearDistanceY)
                if(Mathf.Abs(Player.transform.position.x - transform.position.x) <= NearDistanceX)
                    return true;
        }
        return false;
    }    

}
