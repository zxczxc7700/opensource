using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class HM_Script : MonoBehaviour
{
    private Slider Soundbar;
    private Slider Mousebar;
    private TextMeshProUGUI Soundnum;
    private TextMeshProUGUI Mousenum;
    private double S_num;
    private double M_num;
    // Start is called before the first frame update
    void Start()
    {
        this.Soundbar = GameObject.Find("Sound_slider").GetComponent<Slider>();
        this.Mousebar = GameObject.Find("Mouse_slider").GetComponent<Slider>();
        this.Soundnum = GameObject.Find("Sound_num").GetComponent<TextMeshProUGUI>();
        this.Mousenum = GameObject.Find("Mouse_num").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        S_num = Math.Ceiling(this.Soundbar.value * 100);
        M_num = Math.Ceiling(this.Mousebar.value * 100);
        this.Soundnum.text = (S_num).ToString();
        this.Mousenum.text = (M_num).ToString();
    }
}
