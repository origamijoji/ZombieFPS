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
        BulletDamage = 25;
        ReloadSpeed = 1f;
        FiringRate = 0.25f;
        PointValue = 8;
    }
}
