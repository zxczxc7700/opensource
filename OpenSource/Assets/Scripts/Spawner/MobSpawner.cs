using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public GameObject[] monster;
    public float spawnDelay;

    GameObject spawnMonster;

    float nextSpawnTime;

    void Update()
    {
        Spawn();
    }

    void Spawn()
    {
        if(Time.time > nextSpawnTime)
        {
            nextSpawnTime = Time.time + spawnDelay;

            for(int i = 0; i < 2; i++)
            {
                int num = Random.Range(0, 3);

                spawnMonster = monster[num];

                Instantiate(spawnMonster, transform.position, transform.rotation);
            }
        }
    }
}
