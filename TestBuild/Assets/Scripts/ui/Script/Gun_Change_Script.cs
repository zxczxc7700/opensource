using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gun_Change_Script : MonoBehaviour
{
    private RectTransform W_F;
    private RectTransform W_S;
    private RectTransform W_F_img;
    private RectTransform W_S_img;
    // Start is called before the first frame update
    void Start()
    {
        this.W_F = GameObject.Find("Weapon_First").GetComponent<RectTransform>();
        this.W_S = GameObject.Find("Weapon_Second").GetComponent<RectTransform>();
        this.W_F_img = GameObject.Find("Weapon_img_1").GetComponent<RectTransform>();
        this.W_S_img = GameObject.Find("Weapon_img_2").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            W_F.sizeDelta = new Vector2(320, 150);
            W_S.sizeDelta = new Vector2(220, 80);
            W_F_img.anchoredPosition = new Vector3(-45, 0, 0);
            W_S_img.anchoredPosition = new Vector3(0, 0, 0);

            W_F.anchoredPosition = new Vector3(175, 50, 0);
            W_S.anchoredPosition =new Vector3(175, -70, 0);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            W_F.sizeDelta = new Vector2(220, 80);
            W_S.sizeDelta = new Vector2(320, 150);
            W_F_img.anchoredPosition = new Vector3(0, 0, 0);
            W_S_img.anchoredPosition = new Vector3(-45, 0, 0);

            W_F.anchoredPosition = new Vector3(175, -70, 0);
            W_S.anchoredPosition =new Vector3(175, 50, 0);
        }
    }
}
