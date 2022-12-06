using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gun_Change_Script : MonoBehaviour
{
    Player PLAYER;

    int Weapon1, Weapon2;
    private RectTransform W_F;
    private RectTransform W_S;
    private RectTransform W_F_img;
    private RectTransform W_S_img;
    private Image Weapon1Image;
    private Image Weapon2Image;
    // Start is called before the first frame update
    void Start()
    {
        PLAYER = GameObject.Find("PLAYER").GetComponent<Player>();

        Weapon1 = PLAYER.hasWeapon[0];
        Weapon2 = PLAYER.hasWeapon[1];
        this.Weapon1Image = GameObject.Find("Weapon_img_1").GetComponent<Image>();
        this.Weapon2Image = GameObject.Find("Weapon_img_2").GetComponent<Image>();
        WeaponClassification(Weapon1, Weapon1Image);
        WeaponClassification(Weapon2, Weapon2Image);

        this.W_F = GameObject.Find("Weapon_First").GetComponent<RectTransform>();
        this.W_S = GameObject.Find("Weapon_Second").GetComponent<RectTransform>();
        this.W_F_img = GameObject.Find("Weapon_img_1").GetComponent<RectTransform>();
        this.W_S_img = GameObject.Find("Weapon_img_2").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Weapon1 = PLAYER.hasWeapon[0];
        Weapon2 = PLAYER.hasWeapon[1];
        WeaponClassification(Weapon1, Weapon1Image);
        WeaponClassification(Weapon2, Weapon2Image);

        if (Input.GetKeyDown(KeyCode.Alpha1) && PLAYER.isreload != true)
        {
            W_F.sizeDelta = new Vector2(320, 150);
            W_S.sizeDelta = new Vector2(220, 80);
            W_F_img.anchoredPosition = new Vector3(-45, 0, 0);
            W_S_img.anchoredPosition = new Vector3(0, 0, 0);

            W_F.anchoredPosition = new Vector3(175, 50, 0);
            W_S.anchoredPosition =new Vector3(175, -70, 0);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2) && PLAYER.isreload != true) //조건추가
        {
            W_F.sizeDelta = new Vector2(220, 80);
            W_S.sizeDelta = new Vector2(320, 150);
            W_F_img.anchoredPosition = new Vector3(0, 0, 0);
            W_S_img.anchoredPosition = new Vector3(-45, 0, 0);

            W_F.anchoredPosition = new Vector3(175, -70, 0);
            W_S.anchoredPosition =new Vector3(175, 50, 0);
        }
    }

    void WeaponClassification(int weapon, Image WeaponImage)
    {
        if (weapon >= 0 && weapon <= 2)
        {
            WeaponImage.sprite = Resources.Load<Sprite>("pistol1") as Sprite;
        }
        else if (weapon >= 3 && weapon <= 5)
        {
            WeaponImage.sprite = Resources.Load<Sprite>("pistol2") as Sprite;
        }
        else if (weapon >= 6 && weapon <= 8)
        {
            WeaponImage.sprite = Resources.Load<Sprite>("rifle1") as Sprite;
        }
        else if (weapon >= 9 && weapon <= 11)
        {
            WeaponImage.sprite = Resources.Load<Sprite>("rifle2") as Sprite;
        }
        else if (weapon >= 12 && weapon <= 14)
        {
            WeaponImage.sprite = Resources.Load<Sprite>("rifle3") as Sprite;
        }
        else if (weapon >= 15 && weapon <= 17)
        {
            WeaponImage.sprite = Resources.Load<Sprite>("smg1") as Sprite;
        }
        else if (weapon >= 18 && weapon <= 20)
        {
            WeaponImage.sprite = Resources.Load<Sprite>("smg2") as Sprite;
        }
    }
}
