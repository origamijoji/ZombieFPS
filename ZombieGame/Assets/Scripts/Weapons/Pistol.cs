using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon {
    public Pistol() {
        weaponName = "Pistol";
        reserveAmmo = 42;
        maxReserveAmmo = 42;
        currentMag = 7;
        maxMag = 7;
        drawTime = 0.6f;
        bulletDamage = 25;
        reloadSpeed = 1f;
        firingRate = 0.25f;
    }
}
