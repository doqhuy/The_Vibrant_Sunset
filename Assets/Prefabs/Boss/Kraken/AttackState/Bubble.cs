using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public int Damage = 150;
    public GameObject Explosion;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(Explosion, transform.position, Quaternion.identity);
        if (collision.transform.CompareTag("Player") )
        {
            PlayerCombat PC = collision.transform.GetComponent<PlayerCombat>();
            PC.GetDamage(Damage);
        }
        Destroy(this.gameObject);
    }
}
