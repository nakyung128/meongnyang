using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    private PlayerManager thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerManager>();
    }

    public void NotMove()
    {
        //thePlayer.notMove = true;
    }

    public void Move()
    {
        //thePlayer.notMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
