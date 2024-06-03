using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpellInventory : MonoBehaviour
{
    public PlayerCombat Player;
    public GameObject Content;
    public GameObject Information;

    public Image Image;
    public TMP_Text Name;
    public TMP_Text Description;
    public Button Equip;
    public Button Unequip;
    public Button Recycle;

    public GameObject Weapon;
    public GameObject Spell1;
    public GameObject Spell2;
    void Start()
    {

    }

    void ShowEquip()
    {
        Button wp = Weapon.GetComponent<Button>();
        Button sp1 = Spell1.GetComponent<Button>();
        Button sp2 = Spell2.GetComponent<Button>();

        Image wpImage = wp.transform.Find("Image").gameObject.GetComponent<Image>();
        Image sp1Image = sp1.transform.Find("Image").gameObject.GetComponent<Image>();
        Image sp2Image = sp2.transform.Find("Image").gameObject.GetComponent<Image>();

        TMP_Text wpName = wp.transform.Find("Name").gameObject.GetComponent<TMP_Text>();
        TMP_Text sp1Name = sp1.transform.Find("Name").gameObject.GetComponent<TMP_Text>();
        TMP_Text sp2Name = sp2.transform.Find("Name").gameObject.GetComponent<TMP_Text>();

        if(Player.IsWeaponEquip)
        {
            wpName.gameObject.SetActive(true);
            wp.interactable = true;
            wp.onClick.RemoveAllListeners();
            wp.onClick.AddListener(() =>
            {
                ShowInformation("Weapon");
            });
            wpImage.gameObject.SetActive(true);
            wpImage.sprite = Player.Weapon.Sprite;
            wpName.text = Player.Weapon.Name;
        }
        else
        {
            wpName.gameObject.SetActive(false);
            wp.interactable = false;
            wpImage.gameObject.SetActive(false);
        }

        if (Player.IsSpell1Equip)
        {
            sp1Name.gameObject.SetActive(true);
            sp1.interactable = true;
            sp1.onClick.RemoveAllListeners();
            sp1.onClick.AddListener(() =>
            {
                ShowInformation("Spell1");
            });
            sp1Image.gameObject.SetActive(true);
            sp1Image.sprite = Player.Spell1.Sprite;
            sp1Name.text = Player.Spell1.Name;
        }
        else
        {
            sp1Name.gameObject.SetActive(false);
            sp1.interactable = false;
            sp1Image.gameObject.SetActive(false);
        }

        if (Player.IsSpell2Equip)
        {
            sp2Name.gameObject.SetActive(true);
            sp2.interactable = true;
            sp2.onClick.RemoveAllListeners();
            sp2.onClick.AddListener(() =>
            {
                ShowInformation("Spell2");
            });
            sp2Image.gameObject.SetActive(true);
            sp2Image.sprite = Player.Spell2.Sprite;
            sp2Name.text = Player.Spell2.Name;
        }
        else
        {
            sp2Name.gameObject.SetActive(false);
            sp2.interactable = false;
            sp2Image.gameObject.SetActive(false);
        }
    }

    void ShowSpellInventory()
    {
        for (int i = 0; i < Content.transform.childCount; i++)
        {
            int index = i;
            GameObject item = Content.transform.GetChild(i).gameObject;
            Button button = item.GetComponent<Button>();
            GameObject imagego = item.transform.Find("Image").gameObject;
            Image image = imagego.GetComponent<Image>();
            if (i < Player.Inventory.Spells.Count)
            {
                button.interactable = true;
                imagego.SetActive(true);
                image.sprite = Player.Inventory.Spells[index].Sprite;
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() =>
                {
                    ShowInformation(index);
                });
            }
            else
            {
                button.interactable = false;
                imagego.SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        Information.SetActive(false);
    }


    void ShowInformation(int index)
    {
        Information.SetActive(true);
        Image.sprite = Player.Inventory.Spells[index].Sprite;
        Name.text = Player.Inventory.Spells[index].Name;
        Description.text = Player.Inventory.Spells[index].Description;

        Equip.onClick.RemoveAllListeners();
        Equip.onClick.AddListener(() =>
        {
            Player.EquipSpell(index);
            Information.SetActive(false);
        });

        Recycle.onClick.RemoveAllListeners();
        Recycle.onClick.AddListener(() =>
        {
            Player.RecycleSpell(index);
            Information.SetActive(false);
        });  

        Equip.gameObject.SetActive(true);
        Recycle.gameObject.SetActive(true);
        Unequip.gameObject.SetActive(false);
    }

    void ShowInformation(string place)
    {
        Information.SetActive(true);
        Image.sprite = Player.PointSpell(place).Sprite;
        Name.text = Player.PointSpell(place).Name;
        Description.text = Player.PointSpell(place).Description;

        Unequip .onClick.RemoveAllListeners();
        Unequip.onClick.AddListener(() =>
        {
            Player.UnequipSpell(place);
            Information.SetActive(false);
        });

        Equip.gameObject.SetActive(false);
        Recycle.gameObject.SetActive(false);
        Unequip.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        ShowSpellInventory();
        ShowEquip();
    }
}
