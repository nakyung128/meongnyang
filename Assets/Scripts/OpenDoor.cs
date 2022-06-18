using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField]
    public Dialogue dialogue1;
    public Dialogue dialogue2;
    public Dialogue dialogue3;

    private DialogueManager theDM;
    private Inventory inventory;
    private OrderManager theOrder;

    private bool flag = false;


    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!flag && collision.gameObject.name == "Player" && (!inventory.slots[0].isEmpty && !inventory.slots[1].isEmpty && !inventory.slots[2].isEmpty))
        {
            theOrder.PreLoadCharacter(); // 오더매니저에 있는 리스트에 채워주어야 함
            theOrder.NotMove(); // 움직이지 않도록
            theDM.ShowDialogue(dialogue3);
            
            if (inventory.slots[0].isEmpty && inventory.slots[1].isEmpty && inventory.slots[2].isEmpty)
            {
                theDM.ShowDialogue(dialogue1);
                flag = true;
            }
            theOrder.Move();
        }
        else
        {
            theOrder.PreLoadCharacter(); // 오더매니저에 있는 리스트에 채워주어야 함
            theOrder.NotMove(); // 움직이지 않도록
            if (!flag)
            {
                theDM.ShowDialogue(dialogue2);
            }
            theOrder.Move();
        }
    }
}
