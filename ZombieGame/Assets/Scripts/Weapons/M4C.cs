using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M4C : Weapon {
    public M4C() {
        WeaponName = "M4C";
        Flair = "Full-Auto";
        BulletHoleSize = "Small";
        ReserveAmmo = 80;
        MaxReserveAmmo = 80;
        CurrentMag = 20;
        MaxMag = 20;
        DrawTime = 1.5f;
        BulletDamage = 25;
        DamageFalloff = 20;
        ReloadSpeed = 2f;
        FiringRate = 0.25f;
        PointValue = 3;
        PointMultiplier = 1.1f;
        HeadshotMultiplier = 2f;
        Pierce = false;
        ClipFed = true;
        MaxRange = 20;
        Automatic = true;
        Projectiles = 1;
        BulletSpreadRadius = 0.025f;
        ExplosionRadius = 0;
        ZoomValue = 40;
        ZoomMoveSpeed = 0.7f;
    }
}
