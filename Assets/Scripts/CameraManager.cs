using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject target;   //카메라가 따라갈 대상
    public float moveSpeed;     //카메라 속도
    private Vector3 targetPosition; //대상 현재 위치 값

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target.gameObject != null)  //카메라 대상이 있다면
        {
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);

            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);    //1초에 moveSpeed만큼 이동
        }
    }
}
