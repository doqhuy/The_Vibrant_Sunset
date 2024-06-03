using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHitObject : MonoBehaviour
{
    public string Direct;
    public int Damage = 90;
    void Start()
    {
        if (Direct == "Left")
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
        StartCoroutine(EndOfHit());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerCombat PC = collision.gameObject.GetComponent<PlayerCombat>();
            PC.GetDamage(Damage);
        }
    }

    IEnumerator EndOfHit()
    {
        yield return new WaitForSeconds(0.35f);
        Destroy(this.gameObject);
    }
}
