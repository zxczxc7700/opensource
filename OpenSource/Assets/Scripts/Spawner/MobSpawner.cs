using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public Wave[] wave;
    public GameObject[] monster;

    GameObject spawnMonster;
    Wave curWave;
    int waveCount;
    int nowEnemySpawnCount;

    float nextSpawnTime;


    // Start is called before the first frame update
    void Start()
    {
        SetWave();
    }

    // Update is called once per frame
    void Update()
    {
        Spawn();
        NextWave();
    }

    void SetWave()
    {
        waveCount = 0;
        curWave = wave[waveCount];
        nowEnemySpawnCount = curWave.enemyCount;
        nextSpawnTime = curWave.SpawnDelay;
    }

    void Spawn()
    {
        if(nowEnemySpawnCount > 0 && Time.time > nextSpawnTime)
        {
            nowEnemySpawnCount--;
            nextSpawnTime = Time.time + curWave.SpawnDelay;

            int num = Random.Range(0, 3);

            spawnMonster = monster[num];

            Instantiate(spawnMonster, transform.position, transform.rotation);
        }
    }

    void NextWave()
    {
        if (nowEnemySpawnCount == 0)
        {
            waveCount++;
            if(waveCount < wave.Length)
            {
                curWave = wave[waveCount];
                nowEnemySpawnCount = curWave.enemyCount;
                nextSpawnTime = curWave.SpawnDelay;
            }
        }
    }

    [System.Serializable]
    public class Wave
    {
        public int enemyCount;
        public float SpawnDelay;
    }
}
