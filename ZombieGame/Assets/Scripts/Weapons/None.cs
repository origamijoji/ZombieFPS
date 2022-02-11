using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class None : Weapon
{
    public None() {
        weaponName = "None";
        reserveAmmo = 0;
        maxReserveAmmo = 0;
        currentMag = 0;
        maxMag = 0;
        drawTime = 0;
        bulletDamage = 0;
        reloadSpeed = 0;
        firingRate = 0;
    }

}
