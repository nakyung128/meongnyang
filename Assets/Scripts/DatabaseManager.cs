using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ʿ��� ����
// 1. �� �̵� (�� ���� �ִ� �̺�Ʈ �ٽ� �ߵ��Ǳ� ������ �׷� �� ����)
// 2. ���̺�� �ε�
// 3. �̸� ����� �θ� ���ϴ�! ������

// �ʿ��� ������ ä�������� ��

public class DatabaseManager : MonoBehaviour
{
    static public DatabaseManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }

    public string[] var_name;
    public float[] var;

    public string[] switch_name;
    public bool[] switches;

    // Start is called before the first frame update
    void Start()
    {
        
    }
}
