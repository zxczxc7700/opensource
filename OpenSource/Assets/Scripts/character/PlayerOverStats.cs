using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOverStats : MonoBehaviour
{
    Player player;
    public int coin, kill;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("PLAYER").GetComponent<Player>();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        kill = player.kill;
        coin = player.totalcoin;
    }
}
