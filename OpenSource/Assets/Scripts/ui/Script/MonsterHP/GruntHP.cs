using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GruntHP : MonoBehaviour
{
    public Slider elementalSlider;
	private Transform target;
	private Grunt mon;

	void Start ()
	{
		target = GetComponent<Transform>();
		mon = GetComponent<Grunt>();
	}
	
	void Update ()
	{
		elementalSlider.value = mon.nowHp / mon.maxHp;
	}
}
