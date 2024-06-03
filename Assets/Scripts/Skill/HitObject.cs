using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObject : MonoBehaviour
{
    public string Direct;
    public int Damage = 30;
    void Start()
    {
        if(Direct == "Left")
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }    
        StartCoroutine(EndOfHit());
    }


    IEnumerator EndOfHit()
    {
        yield return new WaitForSeconds(0.35f);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyCombat EC = collision.gameObject.GetComponent<EnemyCombat>();
            EC.GetDamage(Damage);
        }
    }
}
