using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M1911 : Weapon {
    public M1911() {
        WeaponName = "M1911";
        Flair = "Standard Issue";
        BulletHoleSize = "Small";
        ReserveAmmo = 42;
        MaxReserveAmmo = 42;
        CurrentMag = 7;
        MaxMag = 7;
        DrawTime = 0.8f;
        BulletDamage = 20;
        DamageFalloff = 10;
        ReloadSpeed = 1f;
        FiringRate = 0.35f;
        PointValue = 5;
        PointMultiplier = 1.4f;
        HeadshotMultiplier = 1.25f;
        Pierce = false;
        ClipFed = true;
        MaxRange = 15;
        Automatic = false;
        Projectiles = 1;
        BulletSpreadRadius = 0.01f;
        ExplosionRadius = 0;
        ZoomValue = 49;
        ZoomMoveSpeed = 0.8f;
    }
}
