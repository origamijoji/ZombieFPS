using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : Weapon
{
    public Revolver() {
        WeaponName = "Revolver";
        Flair = "High-Caliber";
        BulletHoleSize = "Big";
        ReserveAmmo = 24;
        MaxReserveAmmo = 24;
        CurrentMag = 6;
        MaxMag = 6;
        DrawTime = 0.3f;
        BulletDamage = 35;
        DamageFalloff = 20;
        ReloadSpeed = 0.5f;
        FiringRate = 1f;
        PointValue = 20;
        PointMultiplier = 2f;
        HeadshotMultiplier = 2f;
        Pierce = false;
        ClipFed = false;
        ChamberTime = 1;
        MaxRange = 20;
        Automatic = false;
        Projectiles = 1;
        BulletSpreadRadius = 0.01f;
        ExplosionRadius = 0;
    }
}
