using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonHp_Script : MonoBehaviour
{
    public Slider elementalSlider;
	public Camera camera;
	private Transform target;

	void Start ()
	{
		target = GetComponent<Transform> ();
	}
	
	void Update ()
	{
		Vector3 screenPos = camera.WorldToScreenPoint (target.position);
		float x = screenPos.x;
		elementalSlider.transform.position = new Vector3(x, screenPos.y+50, elementalSlider.transform.position.z);

	}
}
