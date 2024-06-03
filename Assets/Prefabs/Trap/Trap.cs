using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public GameObject Explosion;
    public int Damage = 140;
    public string AnimationName = "FireExplode";
    public enum TrapType { Bomb, Star, Spike, Push}
    public TrapType Type = TrapType.Bomb;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            PlayerCombat PC = collision.transform.GetComponent<PlayerCombat>();
            PC.GetDamage(Damage);
            if(Type == TrapType.Bomb || Type == TrapType.Star)
            {
                SpellAnimation SA = Explosion.GetComponent<SpellAnimation>();
                SA.AnimationName = AnimationName;
                Instantiate(Explosion, transform.position, Quaternion.identity);
                Vector2 pushDirection = collision.transform.position - transform.position;
                pushDirection.Normalize();
                collision.transform.GetComponent<Rigidbody2D>().AddForce(pushDirection * 12f, ForceMode2D.Impulse);
                Destroy(this.gameObject);
            }
            else if(Type == TrapType.Push)
            {
                Vector2 pushDirection = collision.transform.position - transform.position;
                pushDirection.Normalize();
                collision.transform.GetComponent<Rigidbody2D>().AddForce(pushDirection * 20f, ForceMode2D.Impulse);
            }    
            else
            {
                Destroy(this.gameObject);
            }
            
        }
        if (Type == TrapType.Star)
        {
            Destroy(this.gameObject);
            Instantiate(Explosion, transform.position, Quaternion.identity);
        }
    }
}
