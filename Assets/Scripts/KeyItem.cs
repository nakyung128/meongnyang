using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.inputString == (transform.parent.GetComponent<Slot>().num+1).ToString())
        {
            // ������ ���
            Debug.Log((transform.parent.GetComponent<Slot>().num + 1) + "��° ���� ���� ���");
            Destroy(this.gameObject);
        }
    }
}
