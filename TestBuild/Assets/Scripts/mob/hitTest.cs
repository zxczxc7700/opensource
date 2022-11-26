using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class hitTest : MonoBehaviour
{
    //public int maxHp;
    //public int nowHp;

    //bool isDie = false;

    //Rigidbody rigid;
    //SphereCollider sphereCollider;
    //Animator ani;
    //NavMeshAgent nav;


    ////Material mat;
    // void Awake()
    //{
    //    rigid = GetComponent<Rigidbody>();
    //    sphereCollider = GetComponent<SphereCollider>();
    //    ani = GetComponentInChildren<Animator>();
    //    nav = GetComponent<NavMeshAgent>();
    //}

    //void Update()
    //{
        
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.tag == "Bullet" && !isDie)
    //    {
    //        nowHp -= 10;    //damage로 바꾸기
    //        StartCoroutine(OnDamage());
    //        Debug.Log("nowHp : " + nowHp);
    //    }
    //}

    //IEnumerator OnDamage()
    //{
    //    yield return new WaitForSeconds(0.1f);

    //    if(nowHp <= 0)
    //    {
    //        ani.SetTrigger("doDie");
    //        nav.speed = 0;
    //        isDie = true;

    //        Destroy(gameObject, 3);
    //    }
    //}
}
