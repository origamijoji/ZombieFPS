using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon {
    public Rifle() {
        WeaponName = "Rifle";
        BulletHoleSize = "Small";
        ReserveAmmo = 80;
        MaxReserveAmmo = 80;
        CurrentMag = 20;
        MaxMag = 20;
        DrawTime = 1.5f;
        BulletDamage = 25;
        ReloadSpeed = 2f;
        FiringRate = 0.25f;
        PointValue = 3;
        Crit = 2f;
        Pierce = false;
        Automatic = true;
    }
}
