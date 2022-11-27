using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Coin_Script : MonoBehaviour
{
    private TextMeshProUGUI coin;
    private int coinCount;

    Player pStat;

    // Update is called once per frame
    void Start()
    {
        pStat = GameObject.Find("PLAYER").GetComponent<Player>();
        this.coin = GameObject.Find("Text_Coin").GetComponent<TextMeshProUGUI>();
        coinCount = 0;
    }
    
    void Update()
    {
        UpdateCoin();
       //if (Input.GetMouseButtonDown(0))
       //{
       //     coinCount++;
       //     this.coin.text = coinCount.ToString();
       //} 
    }

    void UpdateCoin()
    {
        coinCount = pStat.coin;
        this.coin.text = coinCount.ToString();
    }
    

}
