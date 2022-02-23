using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{
    public Rifle() {
        WeaponName = "Rifle";
        Flair = "AK74";
        BulletHoleSize = "Big";
        ReserveAmmo = 135;
        MaxReserveAmmo = 135;
        CurrentMag = 27;
        MaxMag = 27;
        DrawTime = 2f;
        BulletDamage = 30;
        DamageFalloff = 2.5f;
        ReloadSpeed = 2.5f;
        FiringRate = 0.15f;
        PointValue = 3;
        PointMultiplier = 1.1f;
        HeadshotMultiplier = 2.5f;
        Pierce = false;
        ClipFed = true;
        MaxRange = 45;
        Automatic = true;
        Projectiles = 1;
        BulletSpreadRadius = 0.02f;
        ExplosionRadius = 0;
        ZoomValue = 40;
        ZoomMoveSpeed = 0.7f;
    }
}
