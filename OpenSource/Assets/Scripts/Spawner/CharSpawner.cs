using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharSpawner : MonoBehaviour
{
    public Transform spawnerPosition1;
    public Transform spawnerPosition2;
    public Transform spawnerPosition3;
    GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("PLAYER");
        int seed = Random.Range(1, 4);
        if (seed == 1)
            Player.transform.position = spawnerPosition1.transform.position;
        else if (seed == 2)
            Player.transform.position = spawnerPosition2.transform.position;
        else if (seed == 3)
            Player.transform.position = spawnerPosition3.transform.position;
    }
}
