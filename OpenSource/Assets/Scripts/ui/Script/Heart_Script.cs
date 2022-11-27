using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Heart_Script : MonoBehaviour
{
    private Slider hpbar;
    private int maxHp;
    private int currentHp;
    private TextMeshProUGUI MaxHp;
    private TextMeshProUGUI CurrentHp;
    private Player pStat;

    // Start is called before the first frame update

    void Start()
    {
        pStat = GameObject.Find("PLAYER").GetComponent<Player>();
        this.hpbar = GameObject.Find("Hp_Bar").GetComponent<Slider>();
        this.CurrentHp = GameObject.Find("CurrentHp").GetComponent<TextMeshProUGUI>();
        this.MaxHp = GameObject.Find("MaxHp").GetComponent<TextMeshProUGUI>();
        maxHp = (int)pStat.MaxHP;
        currentHp = (int)pStat.CurHP;
        this.hpbar.value = (float) currentHp/ (float) maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStat();

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    if (currentHp > 0)
        //    {
        //        currentHp -= 10;
        //    }
        //    else
        //    {
        //        currentHp = 0;
        //    }
        //}

        HpHandle();
    }

    void UpdateStat()
    {
        currentHp = (int)pStat.CurHP;

        if (currentHp <= 0)
            currentHp = 0;
    }

    private void HpHandle()
    {
        this.hpbar.value = Mathf.Lerp(hpbar.value, (float)currentHp / (float)maxHp, Time.deltaTime*10);
        this.CurrentHp.text = currentHp.ToString();
    }
}
