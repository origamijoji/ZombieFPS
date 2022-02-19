using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class None : Weapon {
    public None() {
        WeaponName = "None";
        Flair = "None";
        BulletHoleSize = "Small";
        ReserveAmmo = 0;
        MaxReserveAmmo = 0;
        CurrentMag = 0;
        MaxMag = 0;
        DrawTime = 0;
        BulletDamage = 0;
        DamageFalloff = 0;
        ReloadSpeed = 0;
        FiringRate = 0;
        PointValue = 0;
        PointMultiplier = 0f;
        HeadshotMultiplier = 0;
        Pierce = false;
        ClipFed = false;
        MaxRange = 0;
        Automatic = false;
        Projectiles = 0;
        BulletSpreadRadius = 0;
        ExplosionRadius = 0;
        ZoomValue = 0;
        ZoomMoveSpeed = 0f;
    }

}
