using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OverSceneStats : MonoBehaviour
{
    private TextMeshProUGUI kill;
    private TextMeshProUGUI coin;

    PlayerOverStats poStats;
    private int M_kill;
    private int G_coin;

    // Start is called before the first frame update
    void Start()
    {
        poStats = GameObject.Find("poStats").GetComponent<PlayerOverStats>();
        this.kill = GameObject.Find("MKill").GetComponent<TextMeshProUGUI>();
        this.coin = GameObject.Find("GCoin").GetComponent<TextMeshProUGUI>();
        M_kill = poStats.kill;
        G_coin = poStats.coin;
        this.kill.text = M_kill.ToString();
        this.coin.text = G_coin.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
