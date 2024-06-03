using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public PlayerCombat Player;
    public GameObject Content;
    public GameObject Information;

    public Image Image;
    public TMP_Text Name;
    public TMP_Text Description;
    public Button Use;
    public Button Recycle;
    void Start()
    {
        
    }

    void ShowInventory()
    {
        for(int i=0; i<Content.transform.childCount; i++)
        {
            int index = i;
            GameObject item = Content.transform.GetChild(i).gameObject;
            Button button = item.GetComponent<Button>();
            GameObject imagego = item.transform.Find("Image").gameObject;
            Image image = imagego.GetComponent<Image>();
            if (i<Player.Inventory.Items.Count)
            {
                button.interactable = true;
                imagego.SetActive(true);
                image.sprite = Player.Inventory.Items[i].item.Sprite;
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
        Image.sprite = Player.Inventory.Items[index].item.Sprite;
        Name.text = Player.Inventory.Items[index].item.Name;
        Description.text = Player.Inventory.Items[index].item.Description;

        Use.onClick.RemoveAllListeners();
        Use.onClick.AddListener(() =>
        {
            Player.UseItem(index);
            Information.SetActive(false);
        });

        Recycle.onClick.RemoveAllListeners();
        Recycle.onClick.AddListener(() =>
        {
            Player.RecycleItem(index);
            Information.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        ShowInventory();
    }
}
