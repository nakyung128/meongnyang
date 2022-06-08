using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    static public MovingObject instance;    //자기 자신을 값으로 가짐

    public string currentMapName;   //transferMap 스크립트에 있는 transferMapName 변수의 값을 저장

    private BoxCollider2D boxCollider;
    public LayerMask layerMask;     //충돌 layer 판단

    public float speed;

    private Vector3 vector;
    
    public float runSpeed;
    private float applyRunSpeed;
    private bool applyRunFlag = false;

    public int walkCount;
    private int currentWalkCount;

    private bool canMove = true;

    private Animator animator;

    //speed * walkCount = pixel 단위

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);     //player 파괴 방지
            boxCollider = GetComponent<BoxCollider2D>();
            animator = GetComponent<Animator>();    //component 통제
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator MoveCoroutine()
    {
        while(Input.GetAxisRaw("Vertical")!=0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                applyRunSpeed = runSpeed;
                applyRunFlag = true;
            }

            else
            {
                applyRunSpeed = 0;
                applyRunFlag = false;
            }
            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            if (vector.x != 0)
            {
                //vector.x==1이나 -1 일 경우 vector.y=0 으로
                vector.y = 0;
            }
            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            RaycastHit2D hit;   //충돌할경우 hit에 장애물 return
            
            Vector2 start = transform.position;  //캐릭터의 현재 위치 값
            Vector2 end = start + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount);    //캐릭터가 이동하고자 하는 위치 값

            boxCollider.enabled = false;
            hit = Physics2D.Linecast(start, end, layerMask);
            boxCollider.enabled = true;

            //충돌하지 않았을 경우 null return
            if (hit.transform != null)
            {
                break;
            }

            animator.SetBool("Walking", true);

            while (currentWalkCount < walkCount)
            {
                if (vector.x != 0)
                {
                    transform.Translate(vector.x * (speed + applyRunSpeed), 0, 0);
                }
                else if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * (speed + applyRunSpeed), 0);
                }
                if (applyRunFlag)
                {
                    currentWalkCount++;
                }
                currentWalkCount++;
                yield return new WaitForSeconds(0.01f);    //0.01초동안 대기 (반복문 20번:0.2초)
            }
            currentWalkCount = 0;

        }
            animator.SetBool("Walking", false);
            canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Input.GetAxisRaw("Horizontal")
        // 우 방향키가 눌리면 1 리턴, 좌 방향키 눌리면 -1 리턴
        // Input.GetAxisRaw("Vertical")
        // 상 방향키: 1 리턴, 하 방향키: -1 리턴

        // 좌, 우, 상, 하 방향키 눌렸을 경우
        if(canMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false;    //coroutine 반복실행 방지
                StartCoroutine(MoveCoroutine());    //coroutine 실행

            }
        }
    }
}