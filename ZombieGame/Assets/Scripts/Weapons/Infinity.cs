using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infinity : Weapon
{
    public Infinity() {
        WeaponName = "Infinity";
        Flair = "Null-Reference Error";
        BulletHoleSize = "Small";
        ReserveAmmo = 10000000;
        MaxReserveAmmo = 10000000;
        CurrentMag = 10000000;
        MaxMag = 10000000;
        DrawTime = 0f;
        BulletDamage = 10000000;
        DamageFalloff = 0;
        ReloadSpeed = 0f;
        FiringRate = 0f;
        PointValue = 100;
        PointMultiplier = 100f;
        HeadshotMultiplier = 100f;
        Pierce = true;
        ClipFed = true;
        MaxRange = Mathf.Infinity;
        Automatic = true;
        Projectiles = 20;
        BulletSpreadRadius = 0f;
        ExplosionRadius = 0;
    }
}
