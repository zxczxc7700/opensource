using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProcess : MonoBehaviour
{
    GameObject player;
    GameObject playerCamera;
    GameObject mouseSens;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("PLAYER");
        mouseSens = GameObject.Find("MouseSensitivity");
        Destroy(mouseSens);
        Destroy(player);
    }
}
