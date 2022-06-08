using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    static public CameraManager instance;

    public GameObject target;   //ī�޶� ���� ���
    public float moveSpeed;     //ī�޶� �ӵ�
    private Vector3 targetPosition; //��� ���� ��ġ ��

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject); //ī�޶� �ı� ����   
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target.gameObject != null)  //ī�޶� ����� �ִٸ�
        {
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);

            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);    //1�ʿ� moveSpeed��ŭ �̵�
        }
    }
}
