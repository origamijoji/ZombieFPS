using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon {
    public Pistol() {
        WeaponName = "Pistol";
        Flair = "M1911";
        BulletHoleSize = "Small";
        ReserveAmmo = 42;
        MaxReserveAmmo = 42;
        CurrentMag = 7;
        MaxMag = 7;
        DrawTime = 0.8f;
        BulletDamage = 20;
        DamageFalloff = 2.5f;
        ReloadSpeed = 1f;
        FiringRate = 0.35f;
        PointValue = 5;
        PointMultiplier = 1.4f;
        HeadshotMultiplier = 1.25f;
        Pierce = false;
        ClipFed = true;
        MaxRange = 25;
        Automatic = false;
        Projectiles = 1;
        BulletSpreadRadius = 0.01f;
        ExplosionRadius = 0;
        ZoomValue = 49;
        ZoomMoveSpeed = 0.8f;
    }
}
