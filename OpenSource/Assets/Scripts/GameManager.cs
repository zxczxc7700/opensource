using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int count;
    public Transform[] points;

    float timer;
    bool isStop;

    protected virtual void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (isStop)
            return;

        timer += Time.deltaTime;

        if (timer > 0.1f)
        {
            timer = 0f;
            count++;
            Spawn();
        }

        if(count == 1000)
        {
            Time.timeScale = 0.05f;
            isStop = true;
        }
    }

    protected virtual void Spawn()
    {
        //...
    }

    public virtual void ReturnPool(AIPlayer clone)
    {
        //...
    }
}
