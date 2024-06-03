using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScrip : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        yield return new WaitForSeconds(4f);
        GameObject Player = GameObject.Find("Player");
        transform.position = Player.transform.position;
        
    }    
}
