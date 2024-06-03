using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBar : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerCombat PC = collision.gameObject.GetComponent<PlayerCombat>();
            PC.Stat.HP = 0;
        }
    }
}
