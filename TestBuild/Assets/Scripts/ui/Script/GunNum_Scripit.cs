using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GunNum_Scripit : MonoBehaviour
{
    private TextMeshProUGUI gun_num;
    private TextMeshProUGUI max_num;

    WeaponManager gunStat;

    private int maxAmmo;
    private int curAmmo;
    // Start is called before the first frame update
    void Start()
    {
        gunStat = GameObject.Find("PLAYER").GetComponentInChildren<WeaponManager>();
        this.gun_num = GameObject.Find("Current_Num").GetComponent<TextMeshProUGUI>();
        this.max_num = GameObject.Find("Max_Num").GetComponent<TextMeshProUGUI>();
        maxAmmo = gunStat.MaxAmmo;
        curAmmo = gunStat.CurAmmo;
        this.max_num.text = maxAmmo.ToString();
        this.gun_num.text = curAmmo.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAmmo();

       // if (Input.GetKeyDown(KeyCode.Q))
       //{
       //     if(curAmmo>0)
       //     {
       //         curAmmo--;
       //         this.gun_num.text = curAmmo.ToString();
       //     }

       //} 
    }

    void UpdateAmmo()
    {
        curAmmo = gunStat.CurAmmo;
        this.gun_num.text = curAmmo.ToString();
    }
}
