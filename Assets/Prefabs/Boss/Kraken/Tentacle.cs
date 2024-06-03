using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    public Kraken Kraken;

    public GameObject Generate;

    public GameObject LeftCast;
    public GameObject RightCast;

    private void Start()
    {
        StartCoroutine(CastSpell());
    }

    IEnumerator CastSpell()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            EnemyShootObject ESO = Generate.GetComponent<EnemyShootObject>();
            SpellAnimation SA = Generate.GetComponent<SpellAnimation>();
            SA.AnimationName = "WaterShuriken";
            ESO.Direct = "Left";
            Instantiate(Generate, LeftCast.transform.position, Quaternion.identity);
            ESO.Direct = "Right";
            Instantiate(Generate, RightCast.transform.position, Quaternion.identity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Kraken.HP -= 1;
    }
}
