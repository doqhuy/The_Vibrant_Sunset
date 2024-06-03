using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMenu : MonoBehaviour
{

    public PlayerCombat Player;

    public Image Avatar_Form;

    public Slider HPBar_Form;
    public Slider MPBar_Form;

    public TMP_Text HP_Form;
    public TMP_Text MP_Form;

    public Slider HPBar;
    public Slider MPBar;

    public TMP_Text HP;
    public TMP_Text MP;

    public TMP_Text DEF;
    public TMP_Text ATK;

    public Image Weapon;
    public Image Spell1;
    public Image Spell2;

    public Image Avatar;
    public TMP_Text Name;

    // Update is called once per frame
    void Update()
    {
        HPBar_Form.value = (float)Player.Stat.HP / (float)Player.Stat.HPMax;
        MPBar_Form.value = (float)Player.Stat.MP / (float)Player.Stat.MPMax;
        HP_Form.text = "HP: " + Player.Stat.HP + "/" + Player.Stat.HPMax;
        MP_Form.text = "MP: " + Player.Stat.MP + "/" + Player.Stat.MPMax;

        HPBar.value = (float)Player.Stat.HP / (float)Player.Stat.HPMax;
        MPBar.value = (float)Player.Stat.MP / (float)Player.Stat.MPMax;
        HP.text = "HP: " + Player.Stat.HP + "/" + Player.Stat.HPMax;
        MP.text = "MP: " + Player.Stat.MP + "/" + Player.Stat.MPMax;
        DEF.text = "DEF: " + Player.Stat.DEF;
        ATK.text = "ATK: " + Player.Stat.ATK;

        if (Player.IsWeaponEquip)
        {
            Weapon.gameObject.SetActive(true);
            Weapon.sprite = Player.Weapon.Sprite;
        }
        else Weapon.gameObject.SetActive(false);

        if (Player.IsSpell1Equip)
        {
            Spell1.gameObject.SetActive(true);
            Spell1.sprite = Player.Spell1.Sprite;
        }
        else Spell1.gameObject.SetActive(false);

        if (Player.IsSpell2Equip)
        {
            Spell2.gameObject.SetActive(true);
            Spell2.sprite = Player.Spell2.Sprite;
        }
        else Spell2.gameObject.SetActive(false);

        Avatar_Form.sprite = Player.Avatar;
        Avatar.sprite = Player.Avatar;
        Name.text = Player.Name;
    }
}
