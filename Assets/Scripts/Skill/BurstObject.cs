using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstObject : MonoBehaviour
{
    public int Damage = 30;
    void Start()
    {
        StartCoroutine(EndOfHit());
    }


    IEnumerator EndOfHit()
    {
        yield return new WaitForSeconds(0.5f);
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
