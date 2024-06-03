using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public Sprite Avatar;
    public string Name;
    public Stat Stat;
    public Spell Hit;
    public Spell Shoot;

    public GameObject HitCast;
    public GameObject ShootCast;

    public GameObject HitObject;
    public GameObject ShootObject;

    public float AttackCoolDown = 2f;
    public bool IsCoolDown = false;
    public bool IsDameTaking = false;

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
 
    }

 

    public IEnumerator HitAttack()
    {
        Vector3 spawnPosition = HitCast.transform.position;
        SpellAnimation SA = HitObject.GetComponent<SpellAnimation>();
        SA.AnimationName = Hit.Animation;
        EnemyHitObject EHO = HitObject.GetComponent<EnemyHitObject>();
        EHO.Damage = Stat.ATK;
        if (transform.localScale.x < 0) EHO.Direct = "Left";
        else EHO.Direct = "Right";
        Instantiate(HitObject, spawnPosition, Quaternion.identity);
        IsCoolDown = true;
        yield return new WaitForSeconds(Hit.CoolDown);
        IsCoolDown = false;
        
    }

    public IEnumerator ShootAttack()
    {
        Vector3 spawnPosition = ShootCast.transform.position;
        SpellAnimation SA = ShootObject.GetComponent<SpellAnimation>();
        SA.AnimationName = Shoot.Animation;
        EnemyShootObject ESO = ShootObject.GetComponent<EnemyShootObject>();
        ESO.Damage = Stat.ATK;
        if (transform.localScale.x < 0) ESO.Direct = "Left";
        else ESO.Direct = "Right";
        ESO.ExplosionAnimation = Shoot.ExplosionAnimation;
        Instantiate(ShootObject, spawnPosition, Quaternion.identity);
        IsCoolDown = true;
        yield return new WaitForSeconds(Hit.CoolDown);
        IsCoolDown = false;
    }

    public void GetDamage(int Damage)
    {
        if (Stat.HP > 0)
        {
            float floatDamageamage = Damage;
            float DameTake = floatDamageamage * (1f - (float)Stat.DEF / (float)((float)Stat.DEF + 60));
            int RealDameTake = (int)DameTake;
            Stat.HP -= RealDameTake;
            StartCoroutine(DameTakingAction());
        }
    }

    IEnumerator DameTakingAction()
    {
        IsDameTaking = true;
        Animator an = transform.GetComponent<Animator>();
        an.Play("DameTaken");
        yield return new WaitForSeconds(0.25f);
        IsDameTaking = false;
    }


}
