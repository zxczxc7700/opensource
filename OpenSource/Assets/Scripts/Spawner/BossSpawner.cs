using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject Boss;
    public float spawnTime;

    bool isSpawn = false;

    void Update()
    {
        if(!isSpawn)
            Spawn();
    }

    void Spawn()
    {
        if (Time.time > spawnTime)
        {
            Instantiate(Boss, transform.position, transform.rotation);
            isSpawn = true;

        }
    }
}
