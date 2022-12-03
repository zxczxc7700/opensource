using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Silme : MonoBehaviour
{
    public GameObject coin;
    public GameObject getHitEffect;
    public BoxCollider attackRange;
    public int maxHp;
    public int nowHp;

    Vector3 des;
    Rigidbody rigid;
    NavMeshAgent nav;
    Animator ani;
    SphereCollider sphereCollider;
    Player pStat;
    Transform target;


    bool isrun;
    bool isAttack;
    bool isDie;

    void Start()
    {
        pStat = GameObject.Find("PLAYER").GetComponent<Player>();
        target = GameObject.Find("PLAYER").GetComponent<Transform>();
        rigid = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
        ani = GetComponentInChildren<Animator>();
        nav = GetComponent<NavMeshAgent>();
        des = nav.destination;
        NavStart();
    }

    void Update()
    {
        Navigation();
    }

    private void FixedUpdate()
    {
        FreezeVelocity();
        Targetting();
    }

    void NavStart()
    {
        isrun = true;
        ani.SetBool("isRun", true);
    }

    void Navigation()
    {
        if (nav.enabled)
        {
            nav.SetDestination(target.position);
            nav.isStopped = !isrun;
        }

    }

    void FreezeVelocity()
    {
        if (isrun)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }
    void Targetting()
    {
        float targetRadius = 0.5f;
        float targetRange = 1f;

        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player"));   //자신의 위치, 반지름, 나아가는 방향, 범위(거리), 레이아웃

        if(rayHits.Length > 0 && !isAttack)
        {
            StartCoroutine(Attack());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet" && !isDie)
        {
            nowHp -= 10;    //damage로 바꾸기
            StartCoroutine(GetHitEffect());
            StartCoroutine(OnDamage());
        }
    }

    IEnumerator GetHitEffect()
    {
        Vector3 dir = transform.position + new Vector3(0.0f, 1.0f, 0.0f);

        GameObject blood = Instantiate(getHitEffect, dir, transform.rotation);

        yield return new WaitForSeconds(1.0f);

        Destroy(blood);

        yield return null;
    }

    IEnumerator Attack()
    {
        isrun = false;
        ani.SetBool("isRun", false);

        isAttack = true;
        ani.SetBool("isAttack", true);

        yield return new WaitForSeconds(0.2f);

        attackRange.enabled = true;

        yield return new WaitForSeconds(0.5f);

        attackRange.enabled = false;

        isrun = true;
        ani.SetBool("isRun", true);

        isAttack = false;
        ani.SetBool("isAttack", false);
    }

    IEnumerator OnDamage()
    {
        yield return new WaitForSeconds(0.1f);

        if (nowHp <= 0 && !isDie)
        {
            ani.SetTrigger("doDie");
            nav.speed = 0;
            isDie = true;

            Instantiate(coin, transform.position, transform.rotation);

            Destroy(gameObject, 3);
        }
    }
}
