using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProcess2 : MonoBehaviour
{
    GameObject poStats;
    //GameObject mouseSens;
    // Start is called before the first frame update
    void Awake()
    {
        poStats = GameObject.Find("poStats");
        //mouseSens = GameObject.Find("MouseSensitivity");
    }

    private void Start()
    {
        Destroy(poStats);
        //Destroy(mouseSens);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
