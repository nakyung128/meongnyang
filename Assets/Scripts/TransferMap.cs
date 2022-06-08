using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string transferMapName;  //�̵��� ���� �̸�
    
    public Transform target;
    public BoxCollider2D targetBound;

    private MovingObject thePlayer; //MovingObject�� currentMapName �����ϱ� ����
    private CameraManager theCamera;

    // Start is called before the first frame update
    void Start()
    {
        theCamera = FindObjectOfType<CameraManager>();
        thePlayer = FindObjectOfType<MovingObject>();   //�ټ� ��ü ����
    }

    //box collider �� ���� �� ����Ǵ� �Լ�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            thePlayer.currentMapName=transferMapName;
            //SceneManager.LoadScene(transferMapName);
            theCamera.SetBound(targetBound);
            theCamera.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, theCamera.transform.position.z);
            thePlayer.transform.position = target.transform.position;
        }
    }
}
