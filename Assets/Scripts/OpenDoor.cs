using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{

    public Dialogue dialogue_1;
    public Dialogue dialogue_2;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerManager thePlayer; // DirY = 1�� �� (���� �ٶ� ��)
    private FadeManager theFade;

    Inventory inventory;

    private bool flag; // �� �� ���� �ٽ� �� ���� ��. �̰Ŵ� ���߿� ���� ���� ����� ���ƾ� �� ��.

    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
        theFade = FindObjectOfType<FadeManager>();
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!flag && thePlayer.animator.GetFloat("DirY") == 1f) // ĳ���Ͱ� ���� �ٶ� ��
        {
            flag = true; // �����
            StartCoroutine(EventCoroutine());
        }
    }

    IEnumerator EventCoroutine()
    {
        theOrder.PreLoadCharacter(); // �����Ŵ����� �ִ� ����Ʈ�� ä���־�� ��
        theOrder.NotMove(); // �������� �ʵ���

        if (!inventory.slots[0].isEmpty && !inventory.slots[1].isEmpty && !inventory.slots[2].isEmpty)
        {
            theDM.ShowDialogue(dialogue_1);
        }
        else
        {
            theDM.ShowDialogue(dialogue_2);
        }

        // ��ȭ�� ���� ������ ��ٷȴٰ� ��ȭ�� ������ �̵���ų ���̴�.
        yield return new WaitUntil(() => !theDM.talking);

        yield return new WaitUntil(() => thePlayer.queue.Count == 0);

        //theFade.Flash(); �ʿ���� ���� ȿ����

        theOrder.Move();
    }
}
