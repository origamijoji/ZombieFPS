using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carbine : Weapon {
    public Carbine() {
        WeaponName = "Carbine";
        Flair = "M4A1";
        BulletHoleSize = "Small";
        ReserveAmmo = 180;
        MaxReserveAmmo = 180;
        CurrentMag = 30;
        MaxMag = 30;
        DrawTime = 1.5f;
        BulletDamage = 20;
        DamageFalloff = 4;
        ReloadSpeed = 2f;
        FiringRate = 0.08f;
        PointValue = 3;
        PointMultiplier = 1.1f;
        HeadshotMultiplier = 2f;
        Pierce = false;
        ClipFed = true;
        MaxRange = 30;
        Automatic = true;
        Projectiles = 1;
        BulletSpreadRadius = 0.015f;
        ExplosionRadius = 0;
        ZoomValue = 40;
        ZoomMoveSpeed = 0.7f;
        Recoil = 0.7f;
    }
}
