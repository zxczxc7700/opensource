using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public Transform target;
    public GameObject missile;
    public Transform startBullet;
    public Transform missilePortA;
    public Transform missilePortB;
    public BoxCollider attackRange;
    public GameObject tauntEffect;
    public float maxHp;
    public float nowHp;

    Vector3 des;
    Vector3 lookVec;    //플레이어 이동 예측을 위한 벡터
    Vector3 tauntVec;   //taunt공격을 위한 벡터
    Rigidbody rigid;
    NavMeshAgent nav;
    Animator ani;
    BoxCollider boxCollider;
    Player pStat;

    bool isLook;
    bool isDie;

    // Start is called before the first frame update
    void Awake()
    {
        pStat = GameObject.Find("PLAYER").GetComponent<Player>();
        target = GameObject.Find("PLAYER").GetComponent<Transform>();
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        ani = GetComponentInChildren<Animator>();
        nav = GetComponent<NavMeshAgent>();
        des = nav.destination;
        StartCoroutine(SelectPattern());
        nav.isStopped = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDie)
        {
            StopAllCoroutines();
            Invoke("goStage2", 5f);
            return;
        }

        CalLookVec();
    }

    void CalLookVec()
    {
        if (isLook)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            lookVec = new Vector3(h, 0, v) * 5.0f;

            if (lookVec.x > 10.0f)
                lookVec.x = 10.0f;
            if (lookVec.z > 10.0f)
                lookVec.z = 10.0f;

            transform.LookAt(target.position + lookVec);
        }
        else
        {
            nav.SetDestination(tauntVec);
        }
    }

    IEnumerator SelectPattern()
    {
        yield return new WaitForSeconds(1.0f);  //패턴 고르기전에 대기시간

        int randomPattern = Random.Range(0, 4);
        switch (randomPattern)
        {
            //미사일 발사
            case 0:
            case 1:
            case 2:
                StartCoroutine(Taunt());
                break;
            //점프 공격
            case 3:
                StartCoroutine(ShotMissile());
                break;
            default:
                break;
        }
    }

    IEnumerator ShotMissile()
    {
        ani.SetTrigger("doShot");

        yield return new WaitForSeconds(0.2f);

        GameObject instantmissileA = Instantiate(missile, missilePortA.position, missilePortA.rotation);
        BossMissile bossMissileA = instantmissileA.GetComponent<BossMissile>();
        bossMissileA.target = target;

        yield return new WaitForSeconds(0.3f);

        GameObject instantmissileB = Instantiate(missile, missilePortB.position, missilePortB.rotation);
        BossMissile bossMissileB = instantmissileB.GetComponent<BossMissile>();
        bossMissileB.target = target;

        yield return new WaitForSeconds(2f);

        StartCoroutine(SelectPattern());
    }

    IEnumerator Taunt()
    {
        tauntVec = target.position + lookVec;

        isLook = false;
        nav.isStopped = false;

        ani.SetTrigger("doTaunt");

        yield return new WaitForSeconds(1.5f);

        attackRange.enabled = true;

        GameObject tEffect = Instantiate(tauntEffect, transform.position, transform.rotation);

        yield return new WaitForSeconds(0.5f);

        attackRange.enabled = false;

        Destroy(tEffect);

        nav.isStopped = true;

        yield return new WaitForSeconds(1f);

        isLook = true;

        StartCoroutine(SelectPattern());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet" && !isDie)
        {
            nowHp -= (int)pStat.CurWeapon.Damage;    //damage로 바꾸기
            StartCoroutine(OnDamage());
        }
    }

    IEnumerator OnDamage()
    {
        yield return new WaitForSeconds(0.1f);

        if (nowHp <= 0)
        {
            ani.SetTrigger("doDie");
            nav.speed = 0;
            isDie = true;
            pStat.kill++;
            Destroy(gameObject, 6);
        }
    }

    void goStage2()
    {
        SceneManager.LoadScene("s2Loading");
    }
}
