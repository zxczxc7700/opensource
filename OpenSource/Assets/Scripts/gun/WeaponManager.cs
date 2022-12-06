using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public enum Type { pistol1, pistol2, rifle1, rifle2, rifle3, smg1, smg2 };
    public Type type;
    public float Damage;
    public float FireRate;
    public int MaxAmmo;
    public int CurAmmo = 1;

    void Start()
    {
        CurAmmo = MaxAmmo;
    }

    void Update()
    {
        
    }
}
