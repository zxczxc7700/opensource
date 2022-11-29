using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public enum Type { pistol, rifle, smg };
    public Type type;
    public float Damage;
    public float FireRate;
    public int MaxAmmo;
    public int CurAmmo = 1;

    // Start is called before the first frame update
    void Start()
    {
        CurAmmo = MaxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
