using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHP : MonoBehaviour
{
    public Slider elementalSlider;
	private Transform target;
	private Boss mon;

	void Start ()
	{
		target = GetComponent<Transform>();
		mon = GetComponent<Boss>();
	}
	
	void Update ()
	{
		elementalSlider.value = mon.nowHp / mon.maxHp;
	}
}
