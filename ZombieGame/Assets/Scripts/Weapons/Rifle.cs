using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon {
    public Rifle() {
        WeaponName = "Rifle";
        BulletHoleSize = "Big";
        ReserveAmmo = 150;
        MaxReserveAmmo = 150;
        CurrentMag = 30;
        MaxMag = 30;
        DrawTime = 1.5f;
        BulletDamage = 40;
        ReloadSpeed = 2f;
        FiringRate = 0.15f;
        PointValue = 5;
    }
}
