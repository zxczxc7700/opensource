using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTime_Script2 : MonoBehaviour
{
    Player PLAYER; //Ãß°¡µÊ
    private Image img_skill;
    bool checkc = false;
    // Start is called before the first frame update
    void Start()
    {
        PLAYER = GameObject.Find("PLAYER").GetComponent<Player>(); //Ãß°¡µÊ
        this.img_skill = GameObject.Find("img_fill2").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && checkc == false && PLAYER.CurHP != PLAYER.MaxHP) //Á¶°ÇÃß°¡
        {
            StartCoroutine(CoolTime(5f));
            checkc = true;
        }
        if (this.img_skill.fillAmount == 1.0f)
        {
            checkc = false;
        }
    }
    IEnumerator CoolTime(float cool)
    {
        while (cool > 0.0f)
        {
            cool -= Time.deltaTime;
            this.img_skill.fillAmount = 1.0f - cool / 5f;
            yield return new WaitForFixedUpdate();
        }
    }
}
