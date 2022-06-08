using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string transferMapName;  //이동할 맵의 이름
    
    public Transform target;
    public BoxCollider2D targetBound;

    private MovingObject thePlayer; //MovingObject의 currentMapName 참조하기 위해
    private CameraManager theCamera;

    // Start is called before the first frame update
    void Start()
    {
        theCamera = FindObjectOfType<CameraManager>();
        thePlayer = FindObjectOfType<MovingObject>();   //다수 객체 참조
    }

    //box collider 에 닿을 때 실행되는 함수
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
