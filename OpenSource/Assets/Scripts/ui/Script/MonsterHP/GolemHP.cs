using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GolemHP : MonoBehaviour
{
    public Slider elementalSlider;
	private Transform target;
	private Golem mon;

	void Start ()
	{
		target = GetComponent<Transform>();
		mon = GetComponent<Golem>();
	}
	
	void Update ()
	{
		elementalSlider.value = mon.nowHp / mon.maxHp;
	}
}
