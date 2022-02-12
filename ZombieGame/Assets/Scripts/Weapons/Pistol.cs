using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon {
    public Pistol() {
        WeaponName = "M1911";
        BulletHoleSize = "Small";
        ReserveAmmo = 42;
        MaxReserveAmmo = 42;
        CurrentMag = 7;
        MaxMag = 7;
        DrawTime = 0.6f;
        BulletDamage = 20;
        ReloadSpeed = 1f;
        FiringRate = 0.5f;
        PointValue = 5;
        Crit = 1.25f;
        Pierce = false;
        Automatic = false;
    }
}
