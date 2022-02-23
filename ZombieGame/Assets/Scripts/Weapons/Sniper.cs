using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Weapon {
    public Sniper() {
        WeaponName = "Sniper";
        Flair = "Bolt-Action";
        BulletHoleSize = "Big";
        ReserveAmmo = 20;
        MaxReserveAmmo = 20;
        CurrentMag = 5;
        MaxMag = 5;
        DrawTime = 2f;
        BulletDamage = 50;
        DamageFalloff = 1;
        ReloadSpeed = 4f;
        FiringRate = 2f;
        PointValue = 10;
        PointMultiplier = 1f;
        HeadshotMultiplier = 5f;
        Pierce = true;
        ClipFed = true;
        MaxRange = 50;
        Automatic = false;
        Projectiles = 1;
        BulletSpreadRadius = 0.001f;
        ExplosionRadius = 0;
        ZoomValue = 20;
        ZoomMoveSpeed = 0.3f;
    }
}
