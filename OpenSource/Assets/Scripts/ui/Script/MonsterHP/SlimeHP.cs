using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeHP : MonoBehaviour
{
    public Slider elementalSlider;
	private Transform target;
	private Silme mon;

	void Start ()
	{
		target = GetComponent<Transform>();
		mon = GetComponent<Silme>();
	}
	
	void Update ()
	{
		elementalSlider.value = mon.nowHp / mon.maxHp;
	}
}
