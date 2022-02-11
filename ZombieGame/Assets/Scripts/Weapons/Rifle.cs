using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon {
    public Rifle() {
        weaponName = "Rifle";
        reserveAmmo = 150;
        maxReserveAmmo = 150;
        currentMag = 30;
        maxMag = 30;
        drawTime = 1.5f;
        bulletDamage = 40;
        reloadSpeed = 2f;
        firingRate = 0.15f;
    }
}
