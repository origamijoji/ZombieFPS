using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {

    public string weaponName { get; set; }
    public int reserveAmmo { get; set; }
    public int maxReserveAmmo { get; set; }
    public int currentMag { get; set; }
    public int maxMag { get; set; }
    public float drawTime { get; set; }
    public float bulletDamage { get; set; }
    public float reloadSpeed { get; set; }
    public float firingRate { get; set; }

}


