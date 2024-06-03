using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Kraken : MonoBehaviour
{

    public int HP = 20;
    public int ATK = 100;
    Animator animator;

    public GameObject Player;
    public GameObject Tentacle1;
    public GameObject Tentacle2;

    public List<GameObject> RewardGameObject;

    public AudioClip BossAudio;
    public AudioClip OriginAudio;

    public GameObject Bubble1;
    public GameObject Bubble2;
    public GameObject Bubble3;
    public GameObject Bubble4;
    public GameObject Bubble;
    
    private void Start()
    {
        AudioManager AM = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        AM.BossMusic(BossAudio);
        Player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        StartCoroutine(Attack());
        StartCoroutine(TentacleSummon());
    }

    private void Update()
    {
        if(HP <= 0)
        {
            StartCoroutine(ActionDead());
        }
    }

    IEnumerator ActionDead()
    {
        animator.Play("Dead");
        yield return new WaitForSeconds(5f);
        GameObject MainCameraObject = Player.transform.Find("Main Camera").gameObject;
        MainCameraObject.SetActive(true);
        foreach (GameObject go in RewardGameObject)
        {
            go.SetActive(true);
        }
        AudioManager AM = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        AM.BossMusic(OriginAudio);
        Destroy(this.gameObject);
    }

    IEnumerator TentacleSummon()
    {
       
        while (true)
        {
            yield return new WaitForSeconds(13);
            Tentacle1.SetActive(true);
            Tentacle2.SetActive(false);
            yield return new WaitForSeconds(5);
            Tentacle1.SetActive(false);
            yield return new WaitForSeconds(13);
            Tentacle1.SetActive(false);
            Tentacle2.SetActive(true);
            yield return new WaitForSeconds(5);
            Tentacle2.SetActive(false);
            yield return new WaitForSeconds(13);
            Tentacle2.SetActive(true);
            Tentacle1.SetActive(true);
            yield return new WaitForSeconds(5);
            Tentacle2.SetActive(false);
            Tentacle1.SetActive(false);
        }
    }

    IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            if(HP > 0)
            animator.Play("Attack");
            Vector3 spawnPosition1 = Bubble1.transform.position;
            Vector3 spawnPosition2 = Bubble2.transform.position;
            Vector3 spawnPosition3 = Bubble3.transform.position;
            Vector3 spawnPosition4 = Bubble4.transform.position;

            Instantiate(Bubble, spawnPosition1, Quaternion.identity);
            Instantiate(Bubble, spawnPosition2, Quaternion.identity);
            Instantiate(Bubble, spawnPosition3, Quaternion.identity);
            Instantiate(Bubble, spawnPosition4, Quaternion.identity);
        }
    }


}
