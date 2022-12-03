using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Golem : MonoBehaviour
{
    public Transform target;
    public Transform startBullet;
    public BoxCollider attackRange;
    public GameObject bullet;
    public int maxHp;
    public int nowHp;

    Vector3 des;
    Rigidbody rigid;
    NavMeshAgent nav;
    Animator ani;
    SphereCollider sphereCollider;
    Player pStat;


    bool isrun;
    bool isAttack;
    bool isDie;

    // Start is called before the first frame update
    void Awake()
    {
        pStat = GameObject.Find("PLAYER").GetComponent<Player>();
        rigid = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
        ani = GetComponentInChildren<Animator>();
        nav = GetComponent<NavMeshAgent>();
        des = nav.destination;
        NavStart();
    }

    // Update is called once per frame
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
        if(nav.enabled)
        {
            nav.SetDestination(target.position);
            nav.isStopped = !isrun;
        }

    }

    void FreezeVelocity()
    {
        if(isrun)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }

    void Targetting()
    {
        float targetRadius = 0.3f;
        float targetRange = 50f;

        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player"));   //자신의 위치, 반지름, 나아가는 방향, 범위(거리), 레이아웃

        if (rayHits.Length > 0 && !isAttack && !isDie)
        {
            StartCoroutine(Attack());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet" && !isDie)
        {
            nowHp -= 10;    //damage로 바꾸기
            StartCoroutine(OnDamage());
            Debug.Log("nowHp : " + nowHp);
        }
    }

    IEnumerator Attack()
    {
        isrun = false;
        ani.SetBool("isRun", false);

        isAttack = true;
        ani.SetBool("isAttack", true);

        yield return new WaitForSeconds(1.6f);

        Vector3 aim = target.position - startBullet.position;

        aim.y += 1.0f;

        Instantiate(bullet, startBullet.position, Quaternion.LookRotation(aim));

        yield return new WaitForSeconds(1.1f);

        isrun = true;
        ani.SetBool("isRun", true);

        isAttack = false;
        ani.SetBool("isAttack", false);

    }

    IEnumerator OnDamage()
    {
        yield return new WaitForSeconds(0.1f);

        if (nowHp <= 0)
        {
            ani.SetTrigger("doDie");
            nav.speed = 0;
            isDie = true;
            pStat.coin += 5;

            Destroy(gameObject, 3);
        }
    }
}
