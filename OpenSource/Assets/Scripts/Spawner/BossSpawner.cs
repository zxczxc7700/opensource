using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject Boss;
    public float spawnTime;
    float starttime;

    bool isSpawn = false;

    private void Start()
    {
        starttime = Time.time;
    }
    void Update()
    {
        if(!isSpawn)
            Spawn();
    }

    void Spawn()
    {
        if (Time.time - starttime > spawnTime)
        {
            Instantiate(Boss, transform.position, transform.rotation);
            isSpawn = true;
        }
    }
}
