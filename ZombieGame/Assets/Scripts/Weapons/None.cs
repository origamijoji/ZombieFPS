using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class None : Weapon
{
    public None() {
        WeaponName = "None";
        ReserveAmmo = 0;
        MaxReserveAmmo = 0;
        CurrentMag = 0;
        MaxMag = 0;
        DrawTime = 0;
        BulletDamage = 0;
        ReloadSpeed = 0;
        FiringRate = 0;
        PointValue = 0;
    }

}
