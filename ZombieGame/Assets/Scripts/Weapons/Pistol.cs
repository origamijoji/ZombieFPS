using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon {
    public Pistol() {
        WeaponName = "Pistol";
        BulletHoleSize = "Small";
        ReserveAmmo = 42;
        MaxReserveAmmo = 42;
        CurrentMag = 7;
        MaxMag = 7;
        DrawTime = 0.6f;
        BulletDamage = 20;
        DamageFalloff = 10;
        ReloadSpeed = 1f;
        FiringRate = 0.5f;
        PointValue = 5;
        PointMultiplier = 1.4f;
        HeadshotMultiplier = 1.25f;
        Pierce = false;
        MaxRange = 15;
        Automatic = false;
        Projectiles = 1;
        BulletSpreadRadius = 0.01f;
        ExplosionRadius = 0;
    }
}
