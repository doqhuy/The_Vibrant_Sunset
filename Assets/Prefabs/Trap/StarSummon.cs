using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StarSummon : MonoBehaviour
{
    public GameObject Summon;
    public float TimeDelay = 3f;
    void Start()
    {
        
    }

    void OnEnable() 
    {
        StartCoroutine(Consum());
    }


    IEnumerator Consum()
    {
        while (true)
        {
            yield return new WaitForSeconds(TimeDelay);
            Instantiate(Summon, transform.position, Quaternion.identity);
        }
    }
}
