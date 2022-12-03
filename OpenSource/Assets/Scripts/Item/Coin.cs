using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    //public Transform player;
    Rigidbody rigid;
    
    public bool isEnter;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       //Move();
    }

    //void Move()
    //{
    //    if(isEnter)
    //    {
    //        Vector3 dir =  player.position - transform.position;
    //        dir = dir + new Vector3(0f, 1f, 0f);
    //        rigid.AddForce(dir * 0.1f, ForceMode.Impulse);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isEnter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isEnter = false;
        }
    }
}
