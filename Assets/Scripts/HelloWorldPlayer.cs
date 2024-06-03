using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace HelloWorld
{
    public class HelloWorldPlayer : NetworkBehaviour
    {
        public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();
        private Animator animator;
        private Rigidbody2D rb;

        public float moveSpeed = 5f;
        public float jumpHeight = 10f;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

        }
        public override void OnNetworkSpawn()
        {
            if (IsOwner)
            {
                Move();
            }
        }

        public void Move()
        {
            SubmitPositionRequestServerRpc();
        }

        [Rpc(SendTo.Server)]
        void SubmitPositionRequestServerRpc(RpcParams rpcParams = default)
        {
            rb = GetComponent<Rigidbody2D>();
            //var randomPosition = GetRandomPositionOnPlane();
            //transform.position = randomPosition;
            //rb.velocity = randomPosition;
            //rb.velocity = randomPosition;
            Position.Value = rb.velocity;
        }

        static Vector3 GetRandomPositionOnPlane()
        {
            return new Vector3(Random.Range(-3f, 3f), 1f, Random.Range(-3f, 3f));
        }


        void Update()
        {
            if(IsOwner)
            {
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
}
