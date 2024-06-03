using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTalk : MonoBehaviour
{
    public GameObject TalkFrame;
    public string ItemRequire;

    // Update is called once per frame
    void Update()
    {
        GameObject Player = GameObject.Find("Player");
        if (Player != null)
        {
            if (Vector2.Distance(Player.transform.position, transform.position) <= 3f)
            {
                if (TalkFrame != null)
                {
                    TalkFrame.SetActive(true);
                }
                PlayerCombat PC = Player.GetComponent<PlayerCombat>();
                for (int i = 0; i < PC.Inventory.Items.Count; i++)
                {
                    if (PC.Inventory.CheckItemExist(ItemRequire))
                    {
                        Destroy(this.gameObject);
                    }
                }
            }
            else
            {
                if (TalkFrame != null)
                {
                    TalkFrame.SetActive(false);
                }

            }
        }
        

    }
}
