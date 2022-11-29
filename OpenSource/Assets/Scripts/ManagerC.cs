using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class ManagerC : GameManager
{
    PoolManager poolManager;

    protected override void Awake()
    {
        base.Awake();
        poolManager = GetComponent<PoolManager>();
    }

    protected override void Spawn()
    {
        int ran = Random.Range(0, 6);
        AIPlayer newPlayer = poolManager.GetFromPool<AIPlayer>(ran);
    }

    public override void ReturnPool(AIPlayer clone)
    {
        poolManager.TakeToPool<AIPlayer>(clone.idName, clone);
    }
}
