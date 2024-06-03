using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lunar : MonoBehaviour
{
    public GameObject IcePhase1;
    public GameObject IcePhase2;

    public enum LunarType { IcePhase, ShadowBall, BoomBall}
    public LunarType Type;

    public GameObject ShadowBall;
    public GameObject BoomBall;

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        if (Type == LunarType.IcePhase)
        {
            StartCoroutine(IceHandle());
        }
        else if (Type == LunarType.ShadowBall)
        {
            StartCoroutine(ShadowHandle());
        }
        else if (Type == LunarType.BoomBall)
        {
            StartCoroutine(BoomHandle());
        }
    }

    IEnumerator BoomHandle()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.8f);
            SpellAnimation SA = BoomBall.GetComponent<SpellAnimation>();
            SA.AnimationName = "BoomBall";
            EnemyShootObject ESO = BoomBall.GetComponent<EnemyShootObject>();
            ESO.ExplosionAnimation = "FireExplode";
            ESO.Direct = "Left";
            Instantiate(BoomBall, transform.position, Quaternion.identity);
        }
    }

    IEnumerator ShadowHandle()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.8f);
            SpellAnimation SA = ShadowBall.GetComponent<SpellAnimation>();
            SA.AnimationName = "ShadowBall";
            EnemyShootObject ESO = ShadowBall.GetComponent<EnemyShootObject>();
            ESO.ExplosionAnimation = "DarkExplode";
            ESO.Direct = "Right";
            Instantiate(ShadowBall, transform.position, Quaternion.identity);
        }
    }

    IEnumerator IceHandle()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            IcePhase1.SetActive(true);
            IcePhase2.SetActive(false);
            yield return new WaitForSeconds(3f);
            IcePhase1.SetActive(false);
            IcePhase2.SetActive(true);
        }
    }
}
