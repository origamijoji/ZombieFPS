using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Weapon {
    public Sniper() {
        WeaponName = "Sniper";
        BulletHoleSize = "Big";
        ReserveAmmo = 20;
        MaxReserveAmmo = 20;
        CurrentMag = 5;
        MaxMag = 5;
        DrawTime = 3f;
        BulletDamage = 50;
        DamageFalloff = 0;
        ReloadSpeed = 6f;
        FiringRate = 4f;
        PointValue = 10;
        HeadshotMultiplier = 5f;
        Pierce = true;
        MaxRange = 50;
        Automatic = false;
        Projectiles = 1;
        BulletSpreadRadius = 0.001f;
        ExplosionRadius = 0;
    }
}
