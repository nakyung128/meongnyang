using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event1 : MonoBehaviour
{

    public Dialogue dialogue_1;
    public Dialogue dialogue_2;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerManager thePlayer; // DirX = -1일 때 (왼쪽 바라볼 때)

    private bool flag; // 한 번 보면 다시 못 보게 함. 이거는 나중에 뺄지 말지 얘기해 보아야 할 듯.

    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!flag && Input.GetKey(KeyCode.Z) && thePlayer.animator.GetFloat("DirX") == -1f) // z를 눌렀을 때만
        {
            flag = true; // 실행됨
            StartCoroutine(EventCoroutine());
        }
    }

    IEnumerator EventCoroutine()
    {
        theOrder.PreLoadCharacter(); // 오더매니저에 있는 리스트에 채워주어야 함
        theOrder.NotMove(); // 움직이지 않도록

        theDM.ShowDialogue(dialogue_1);

        // 대화가 끝날 때까지 기다렸다가 대화가 끝나면 이동시킬 것이다.
        yield return new WaitUntil(() => !theDM.talking);

        yield return new WaitUntil(() => thePlayer.queue.Count == 0);

        theOrder.Move();
    }
}
