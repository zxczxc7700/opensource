using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss2HP : MonoBehaviour
{
    public Slider elementalSlider;
	private Transform target;
	private Boss2 mon;

	void Start ()
	{
		target = GetComponent<Transform>();
		mon = GetComponent<Boss2>();
	}
	
	void Update ()
	{
		elementalSlider.value = mon.nowHp / mon.maxHp;
	}
}
