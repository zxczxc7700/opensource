using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Redcode.Pools;

public class AIPlayer : MonoBehaviour, IPoolObject
{
    public string idName;
    //public Animator anim;
    public Vector3 targetPos;
    //bool isAtDestination;

    //NavMeshAgent ai;

    void Awake()
    {
       // ai = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        //Vector3 relVelocity = transform.InverseTransformDirection(ai.velocity);
        //relVelocity.y = 0;

        //anim.SetFloat("NormalizedSpeed", relVelocity.magnitude / anim.transform.lossyScale.x);

        //if (ai.remainingDistance < 2f)
        //{
        //    if (!isAtDestination)
        //        OnTargetReached();

        //    isAtDestination = true;
        //}
        //else
        //{
        //    isAtDestination = false;
        //}
    }

    void OnTargetReached()
    {
        GameManager.instance.ReturnPool(this);
    }

    void Init()
    {
        transform.position = new Vector3(Random.Range(-2f, 2f), 0.7f, Random.Range(-2f, 2f));
        Transform[] spawnPos = GameManager.instance.points;
        //ai.SetDestination(spawnPos[Random.Range(0, spawnPos.Length)].position);
    }

    public void OnCreatedInPool()
    {

    }

    public void OnGettingFromPool()
    {
        Init();
    }
}
