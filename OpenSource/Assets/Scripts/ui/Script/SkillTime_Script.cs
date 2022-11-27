using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTime_Script : MonoBehaviour
{
    private Image img_skill;
    // Start is called before the first frame update
    void Start()
    {
        this.img_skill = GameObject.Find("img_fill").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(CoolTime(5f));
        }
    }
    IEnumerator CoolTime (float cool)
    {
        while (cool > 0.0f)
        {
            cool -= Time.deltaTime;
            this.img_skill.fillAmount = 1.0f - cool/5.0f;
            yield return new WaitForFixedUpdate();
        }
    }
}
