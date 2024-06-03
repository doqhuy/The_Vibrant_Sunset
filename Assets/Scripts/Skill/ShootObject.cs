using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootObject : MonoBehaviour
{
    Rigidbody2D rb;
    public string Direct = "Right";
    public string ExplosionAnimation = "FireExplode";
    public float acceleration = 3.1f; // Giá trị tăng tốc
    private float speed = 8.0f;
    public int Damage = 30;
    public GameObject Explosion;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(Direct == "Left")
        {
            Vector3 scale = transform.localScale;
            transform.localScale = scale * (-1);
        }    
        StartCoroutine(EndOfCast());
    }


    IEnumerator EndOfCast()
    {
        yield return new WaitForSeconds(6);
        Vector3 spawnPosition = transform.position;
        SpellAnimation SA = Explosion.GetComponent<SpellAnimation>();
        SA.AnimationName = ExplosionAnimation;
        Instantiate(Explosion, spawnPosition, Quaternion.identity);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 spawnPosition = transform.position;
        SpellAnimation SA = Explosion.GetComponent<SpellAnimation>();
        SA.AnimationName = ExplosionAnimation;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyCombat EC = collision.gameObject.GetComponent<EnemyCombat>();
            EC.GetDamage(Damage);
        }
        Instantiate(Explosion, spawnPosition, Quaternion.identity);
        AudioManager AM = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        AM.ExplosionSound();
        Destroy(this.gameObject);
    }

    void Update()
    {
        speed += acceleration * Time.deltaTime;
        if(Direct == "Right")
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-1 * speed, rb.velocity.y);
        }
    }
}
