using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DemonEye : MonoBehaviour
{
    public GameObject Lunar1;
    public GameObject Lunar2;
    public GameObject Lunar3;

    public GameObject Eye1;
    public GameObject Eye2;
    public GameObject Eye3;

    public GameObject DeadBomb;

    public GameObject BossObject;

    public int HP = 50;
    private bool IsDead = false;
    void Start()
    {
        StartCoroutine(Lunar1Handle());
        StartCoroutine(Lunar2Handle());
        StartCoroutine(Lunar3Handle());
        StartCoroutine(EyeAppear());
    }

    IEnumerator EyeAppear()
    {
        while(HP>0)
        {
            yield return new WaitForSeconds(10f);
            if(HP>0)
            {
                Eye1.SetActive(true);
                Eye2.SetActive(true);
                Eye3.SetActive(true);
            }
            yield return new WaitForSeconds(7f);
            Eye1.SetActive(false);
            Eye2.SetActive(false);
            Eye3.SetActive(false);

        }
    }

    private void Update()
    {
        if(HP<=0 && IsDead == false)
        {
            IsDead = true;
            StartCoroutine(DeadAction());
        }
    }

    IEnumerator DeadAction()
    {
        Lunar1.SetActive(false);
        Lunar2.SetActive(false);
        Lunar3.SetActive(false);
        SpellAnimation SA = DeadBomb.GetComponent<SpellAnimation>();
        SA.AnimationName = "FireExplode";
        Instantiate(DeadBomb, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        yield return new WaitForSeconds(0.5f);

        SA.AnimationName = "DarkExplode";
        Instantiate(DeadBomb, new Vector3(transform.position.x + 3, transform.position.y + 3, transform.position.z), Quaternion.identity);
        yield return new WaitForSeconds(0.5f);

        SA.AnimationName = "ToxicExplode";
        Instantiate(DeadBomb, new Vector3(transform.position.x - 3, transform.position.y - 3, transform.position.z), Quaternion.identity);
        yield return new WaitForSeconds(0.5f);

        SA.AnimationName = "DarkExplode";
        Instantiate(DeadBomb, new Vector3(transform.position.x - 1, transform.position.y - 1, transform.position.z), Quaternion.identity);
        yield return new WaitForSeconds(4f);
        GameObject Player = GameObject.Find("Player");
        GameObject Camera = Player.transform.Find("Main Camera").gameObject;
        BossObject.SetActive(true);
        Camera.SetActive(true);
        Destroy(this.gameObject);
    }

    IEnumerator Lunar1Handle()
    {
        while (HP > 0)
        {
            yield return new WaitForSeconds(3f);
            if (HP > 0)
            Lunar1.SetActive(true);
            yield return new WaitForSeconds(5f);
            Lunar1.SetActive(false);
        }
    }

    IEnumerator Lunar2Handle()
    {
        while (HP > 0)
        {
            yield return new WaitForSeconds(5f);
            if(HP>0)
            Lunar2.SetActive(true);
            yield return new WaitForSeconds(4f);
            Lunar2.SetActive(false);
        }
    }

    IEnumerator Lunar3Handle()
    {
        while (HP > 0)
        {
            yield return new WaitForSeconds(8f);
            if (HP > 0)
            Lunar3.SetActive(true);
            yield return new WaitForSeconds(6f);
            Lunar3.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.transform.CompareTag("Ground"))
        {
            HP--;
        }
    }
}
