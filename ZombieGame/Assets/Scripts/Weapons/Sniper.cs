using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Weapon
{
    public Sniper() {
        WeaponName = "Sniper";
        BulletHoleSize = "Big";
        ReserveAmmo = 40;
        MaxReserveAmmo = 40;
        CurrentMag = 5;
        MaxMag = 5;
        DrawTime = 3f;
        BulletDamage = 50;
        ReloadSpeed = 6f;
        FiringRate = 4f;
        PointValue = 10;
        Crit = 5f;
        Pierce = true;
        Automatic = false;
    }
}
