using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField]
    public Sprite Avatar;
    public string Name;

    public Stat Stat;
    public int Coin;
    public int Ammo;

    public Spell Weapon;
    public Spell Spell1 = null;
    public Spell Spell2 = null;

    public bool IsWeaponEquip = false;
    public bool IsSpell1Equip = false;
    public bool IsSpell2Equip = false;


    public GameObject HitCast;
    public GameObject Spell1Cast;
    public GameObject Spell2Cast;

    public GameObject HitObject;
    public GameObject ShootObject;
    public GameObject BurstObject;


    [SerializeField]
    public Inventory Inventory;
    public string[] HitAnimation = { "WaterHit1", "WaterHit2", "WaterHit3" };
    private int HitNow;

    public bool CanHit = true;
    public bool CanSpell1 = true;
    public bool CanSpell2 = true;
    public bool CanMissile = true;

    public bool IsDameTaking = false;
    private bool IsDeadSound = false;

    public GameObject Ultimate;
    private bool IsUltimate = false;
    public bool IsProtect = false;

    public bool IsLeft;
    public bool IsRight;
    public bool IsJump;
    void Start()
    {
        HitNow = 0;
        
    }

    // Update is called once per frame
    void Update()
    {

                HandleEquipment();
                if (Stat.MP >= Stat.MPMax)
                {
                    Stat.MP = 0;
                    StartCoroutine(UltimateArrive());
                }
        
         
        
    }

    IEnumerator UltimateArrive()
    {
        IsUltimate = true;
        Ultimate.SetActive(true);
        yield return new WaitForSeconds(20f);
        Ultimate.SetActive(false);
        IsUltimate = false;
    }

    void HandleEquipment()
    {
        int ATKStat = 0;
        int DEFStat = 0;
        if(IsWeaponEquip)
        {
            HitAnimation[0] = Weapon.HitAnimation;
            ATKStat += Weapon.ATK;
            DEFStat += Weapon.DEF;
        }
        else
        {
            HitAnimation[0] = "WhiteHit1";
        }
        if(IsSpell1Equip)
        {
            HitAnimation[1] = Spell1.HitAnimation;
            ATKStat += Spell1.ATK;
            DEFStat += Spell1.DEF;
        }
        else
        {
            HitAnimation[1] = "WhiteHit2";

        }
        if (IsSpell2Equip)
        {
            HitAnimation[2] = Spell2.HitAnimation;
            ATKStat += Spell2.ATK;
            DEFStat += Spell2.DEF;
        }
        else
        {
            HitAnimation[2] = "WhiteHit3";
        }

        if(IsUltimate)
        {
            Stat.ATK = ATKStat * 2 + Stat.BaseATK;
            Stat.DEF = DEFStat * 2 + Stat.BaseDEF;
        }
        else
        {
            Stat.ATK = ATKStat + Stat.BaseATK;
            Stat.DEF = DEFStat + Stat.BaseDEF;
        }
        
    }


    public void HitObjectCast()
    {
        Vector3 spawnPosition = HitObject.transform.position;
        SpellAnimation SA = HitCast.GetComponent<SpellAnimation>();
        SA.AnimationName = HitAnimation[HitNow];
        HitNow++;
        if(HitNow >= HitAnimation.Length)
        {
            HitNow = 0;
        }    
        if (transform.localScale.x < 0)
        {
            HitObject HO = HitCast.GetComponent<HitObject>();
            HO.Damage = Stat.ATK * Weapon.Scale / 100;
            HO.Direct = "Left";
        }
        else
        {
            HitObject HO = HitCast.GetComponent<HitObject>();
            HO.Damage = Stat.ATK * Weapon.Scale / 100;
            HO.Direct = "Right";
        }
        Instantiate(HitCast, spawnPosition, Quaternion.identity);
        StartCoroutine(CoolDownHit());
    }

    public IEnumerator CoolDownHit()
    {
        CanHit = false;
        yield return new WaitForSeconds(0.25f);
        CanHit = true;
    }
    public IEnumerator CoolDownSpell1()
    {
        CanSpell1 = false;
        yield return new WaitForSeconds(Spell1.CoolDown);
        CanSpell1 = true;
    }

    public IEnumerator CoolDownSpell2()
    {
        CanSpell2 = false;
        yield return new WaitForSeconds(Spell2.CoolDown);
        CanSpell2 = true;
    }

    public IEnumerator CoolDownMissile()
    {
        CanMissile = false;
        yield return new WaitForSeconds(1f);
        CanMissile = true;
    }


    public IEnumerator BurstObjectCast()
    {
        StartCoroutine(CoolDownSpell2());
        SpellAnimation SA = Spell2Cast.GetComponent<SpellAnimation>();
        SA.AnimationName = Spell2.Animation;
        BurstObject BO = Spell2Cast.GetComponent<BurstObject>();
        BO.Damage = Stat.ATK * Spell2.Scale / 100;
        yield return new WaitForSeconds(0.4f);
        Instantiate(Spell2Cast, BurstObject.transform.position, Quaternion.identity);
    }

    public IEnumerator ShootObjectCast()
    {
        StartCoroutine(CoolDownSpell1());
        SpellAnimation SA = Spell1Cast.GetComponent<SpellAnimation>();
        SA.AnimationName = Spell1.Animation;
        yield return new WaitForSeconds(0.4f);
        Vector3 spawnPosition = ShootObject.transform.position;
        if(transform.localScale.x < 0)
        {
            ShootObject SO = Spell1Cast.GetComponent<ShootObject>();
            SO.Damage = Stat.ATK * Weapon.Scale / 100;
            SO.ExplosionAnimation = Spell1.ExplosionAnimation;
            SO.Direct = "Left";
        }
        else
        {
            ShootObject SO = Spell1Cast.GetComponent<ShootObject>();
            SO.Damage = Stat.ATK * Weapon.Scale / 100;
            SO.ExplosionAnimation = Spell1.ExplosionAnimation;
            SO.Direct = "Right";
        }    
        Instantiate(Spell1Cast, spawnPosition, Quaternion.identity);
    }

    public IEnumerator MissileObjectCast()
    {
        StartCoroutine(CoolDownMissile());
        SpellAnimation SA = Spell1Cast.GetComponent<SpellAnimation>();
        SA.AnimationName = "Missile";
        yield return new WaitForSeconds(0.4f);
        Vector3 spawnPosition = ShootObject.transform.position;
        if (transform.localScale.x < 0)
        {
            ShootObject SO = Spell1Cast.GetComponent<ShootObject>();
            SO.Damage = Stat.ATK * 3;
            SO.ExplosionAnimation = "FireExplode";
            SO.Direct = "Left";
        }
        else
        {
            ShootObject SO = Spell1Cast.GetComponent<ShootObject>();
            SO.Damage = Stat.ATK * 3;
            SO.ExplosionAnimation = "FireExplode";
            SO.Direct = "Right";
        }
        Instantiate(Spell1Cast, spawnPosition, Quaternion.identity);
    }

    public void GetItem(Item item, int stack)
    {
        Inventory.AddItem(item, stack);
    }

    public void UseItem(int index)
    {
        if (Inventory.Items[index].item.Type == "Heal")
        {
            Stat.HP += Inventory.Items[index].item.HP;
            Stat.MP += Inventory.Items[index].item.MP;
            if(Stat.HP > Stat.HPMax) Stat.HP = Stat.HPMax;
            if(Stat.MP > Stat.MPMax) Stat.MP = Stat.MPMax;
            Inventory.Items[index].stack--;
            if(Inventory.Items[index].stack <= 0) Inventory.Items.RemoveAt(index);
        }
    }

    public void RecycleItem(int index)
    {
        Inventory.Items.RemoveAt(index);
        Coin++;
    }    

    public void GetSpell(Spell spell)
    {
        Inventory.AddSpell(spell);
    }    

    public Spell PointSpell(string place)
    {
        switch(place)
        {
            case "Weapon": return Weapon;
            case "Spell1": return Spell1;
            case "Spell2": return Spell2;
        }
        return null;
    }    

    public void EquipSpell(int index)
    {
        if (Inventory.Spells[index].Type == Spell.SkillType.Shoot)
        {
            if(IsSpell1Equip)
            {
                UnequipSpell("Spell1");
            }    
            IsSpell1Equip = true;
            Spell1 = Inventory.Spells[index];
        }    
        else if (Inventory.Spells[index].Type == Spell.SkillType.Burst)
        {
            if (IsSpell2Equip)
            {
                UnequipSpell("Spell2");
            }
            IsSpell2Equip = true;
            Spell2 = Inventory.Spells[index];
        }
        else if (Inventory.Spells[index].Type == Spell.SkillType.Weapon)
        {
            if (IsWeaponEquip)
            {
                UnequipSpell("Weapon");
            }
            IsWeaponEquip = true;
            Weapon = Inventory.Spells[index];
        }
        Inventory.Spells.RemoveAt(index);
    }

    public void UnequipSpell(string place)
    {
        switch (place)
        {
            case "Weapon": IsWeaponEquip = false; Inventory.Spells.Add(Weapon); break;
            case "Spell1": IsSpell1Equip = false; Inventory.Spells.Add(Spell1); break;
            case "Spell2": IsSpell2Equip = false; Inventory.Spells.Add(Spell2); break;
        }    
    }

    public void RecycleSpell(int index)
    {
        Inventory.Spells.RemoveAt(index);
        Ammo++;
    }    

    public IEnumerator DeadAction()
    {
        if(!IsDeadSound)
        {
            AudioManager AM = GameObject.Find("AudioManager").GetComponent<AudioManager>();
            AM.DeadSound();
        }
        IsDeadSound = true;
        Animator an = transform.GetComponent<Animator>();
        an.Play("Dead");
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("Dead");
    }    

    public void GetDamage(int Damage)
    {
        if (IsProtect) Damage -= Damage / 2;
        if(Stat.HP >0)
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
        AudioManager AM = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        AM.HurtSound();
        IsDameTaking = true;
        Animator an = transform.GetComponent<Animator>();
        an.Play("DameTaken");
        yield return new WaitForSeconds(0.25f);
        IsDameTaking = false;
    }    
}




[System.Serializable]
public class Stat
{
    public int HP = 500;
    public int MP = 250;
    public int ATK = 120;
    public int DEF = 100;

    public int HPMax = 500;
    public int MPMax = 250;
    public int BaseATK = 120;
    public int BaseDEF = 100;
}

[System.Serializable]
public class Inventory
{
    public List<Spell> Spells = new List<Spell>();
    public List<Items> Items = new List<Items>();

    

    public void AddSpell(Spell spell)
    {
        Spells.Add(spell);
    }

    public void AddItem(Item item, int stack)
    {
        if(CheckItemExist(item))
        {
            foreach (Items i in Items)
            {
                if (i.item.Name == item.Name)
                {
                    i.stack += stack;
                }
            }
        }    
        else
        {
            Items.Add(new Items(item, stack));
        }    
    }

    bool CheckItemExist(Item item)
    {
        foreach(Items i in Items)
        {
            if(i.item.Name == item.Name)
            {
                return true;
            }    
        }    
        return false;
    }

    public bool CheckItemExist(string itemname)
    {
        foreach (Items i in Items)
        {
            if (i.item.Name == itemname)
            {
                return true;
            }
        }
        return false;
    }

}

[System.Serializable]
public class Spell
{
    public string Name;
    public string Description;
    public Sprite Sprite;
    public string Animation = "FireBall";
    public string ExplosionAnimation = "FireExplode";
    public int Scale = 150;
    public float CoolDown = 5f;
    public enum SkillType { Shoot, Burst, Dash, Point, Fall, Weapon }
    public SkillType Type;

    public string HitAnimation = "WaterHit1";
    public int ATK = 20;
    public int DEF = 20;

}

[System.Serializable]
public class Item
{
    public Sprite Sprite;
    public string Name;
    public string Description;
    public int HP = 20;
    public int MP = 20;
    public string Type = "Heal";
}

[System.Serializable]
public class Items
{
    public Item item;
    public int stack = 1;

    public Items(Item item, int stack)
    {
        this.item = item;
        this.stack = stack;
    }
}

