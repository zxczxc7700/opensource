using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rock : MonoBehaviour
{
    Rigidbody rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.AddForce(transform.forward * 15, ForceMode.Impulse);
    }

    void Update()
    {
        transform.Rotate(Vector3.right * 60 * Time.deltaTime);
    }
}
