using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraYmove : MonoBehaviour
{
    public float sensitiviy = 400f;
    //public Transform objectTofollow;

    private float rotX;
    //private float rotY;

    // Start is called before the first frame update
    void Start()
    {
        rotX = transform.localRotation.eulerAngles.x;
        sensitiviy = GameObject.Find("MouseSensitivity").GetComponent<MouseSensitivity>().Sens * 400f;
    }

    // Update is called once per frame
    void Update()
    {
        rotX += -Input.GetAxis("Mouse Y") * sensitiviy * Time.deltaTime;
        //rotY += Input.GetAxis("Mouse X") * 400f * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -25f, 40f);
        Quaternion rot = Quaternion.Euler(rotX, 0, 0);
        transform.localRotation = rot; 
    }
}
