using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    static public MovingObject instance;    //�ڱ� �ڽ��� ������ ����

    public string currentMapName;   //transferMap ��ũ��Ʈ�� �ִ� transferMapName ������ ���� ����

    private BoxCollider2D boxCollider;
    public LayerMask layerMask;     //�浹 layer �Ǵ�

    public float speed;

    private Vector3 vector;
    
    public float runSpeed;
    private float applyRunSpeed;
    private bool applyRunFlag = false;

    public int walkCount;
    private int currentWalkCount;

    private bool canMove = true;

    private Animator animator;

    //speed * walkCount = pixel ����

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);     //player �ı� ����
            boxCollider = GetComponent<BoxCollider2D>();
            animator = GetComponent<Animator>();    //component ����
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
                //vector.x==1�̳� -1 �� ��� vector.y=0 ����
                vector.y = 0;
            }
            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            RaycastHit2D hit;   //�浹�Ұ�� hit�� ��ֹ� return
            
            Vector2 start = transform.position;  //ĳ������ ���� ��ġ ��
            Vector2 end = start + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount);    //ĳ���Ͱ� �̵��ϰ��� �ϴ� ��ġ ��

            boxCollider.enabled = false;
            hit = Physics2D.Linecast(start, end, layerMask);
            boxCollider.enabled = true;

            //�浹���� �ʾ��� ��� null return
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
                yield return new WaitForSeconds(0.01f);    //0.01�ʵ��� ��� (�ݺ��� 20��:0.2��)
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
        // �� ����Ű�� ������ 1 ����, �� ����Ű ������ -1 ����
        // Input.GetAxisRaw("Vertical")
        // �� ����Ű: 1 ����, �� ����Ű: -1 ����

        // ��, ��, ��, �� ����Ű ������ ���
        if(canMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false;    //coroutine �ݺ����� ����
                StartCoroutine(MoveCoroutine());    //coroutine ����

            }
        }
    }
}