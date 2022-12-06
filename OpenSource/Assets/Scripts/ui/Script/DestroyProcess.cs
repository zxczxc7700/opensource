using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProcess : MonoBehaviour
{
    //GameObject player;
    GameObject mouseSens;
    // Start is called before the first frame update
    void Awake()
    {
        //player = GameObject.Find("PLAYER");
        mouseSens = GameObject.Find("MouseSensitivity");
    }

    private void Start()
    {
        //Destroy(player);
        Destroy(mouseSens);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
