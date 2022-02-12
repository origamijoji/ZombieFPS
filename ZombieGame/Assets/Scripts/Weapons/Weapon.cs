using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {

    public string WeaponName { get; set; }
    public string BulletHoleSize { get; set; }
    public int ReserveAmmo { get; set; }
    public int MaxReserveAmmo { get; set; }
    public int CurrentMag { get; set; }
    public int MaxMag { get; set; }
    public float DrawTime { get; set; }
    public float BulletDamage { get; set; }
    public float ReloadSpeed { get; set; }
    public float FiringRate { get; set; }
    public int PointValue { get; set; }

}


