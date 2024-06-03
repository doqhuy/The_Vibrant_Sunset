using UnityEngine;

public class DropObject : MonoBehaviour
{
    public GameObject player;  // Đối tượng người chơi
    public float detectionRange = 5.0f;  // Tầm kiểm tra
    public enum DropType { Item, Spell}
    public DropType Type;

    public Spell Spell;
    public Items Item;
    
    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (player != null)
        {
            PlayerCombat CF = player.GetComponent<PlayerCombat>();
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance <= detectionRange)
            {
                transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime * 2.0f);
                if (distance <= 1f)
                {
                    switch(Type)
                    {
                        case DropType.Item: CF.GetItem(Item.item, Item.stack); break;
                        case DropType.Spell: CF.GetSpell(Spell); break;
                    }    
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
