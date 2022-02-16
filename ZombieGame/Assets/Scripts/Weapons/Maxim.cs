using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maxim : Weapon
{
    public Maxim() {
        WeaponName = "Maxim";
        BulletHoleSize = "Small";
        ReserveAmmo = 400;
        MaxReserveAmmo = 400;
        CurrentMag = 100;
        MaxMag = 100;
        DrawTime = 4f;
        BulletDamage = 8;
        DamageFalloff = 20;
        ReloadSpeed = 3f;
        FiringRate = 0.08f;
        PointValue = 1;
        PointMultiplier = 1.3f;
        HeadshotMultiplier = 1.2f;
        Pierce = false;
        MaxRange = 10;
        Automatic = true;
        Projectiles = 1;
        BulletSpreadRadius = 0.08f;
        ExplosionRadius = 0;
    }
}
