using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon {
    public string WeaponName { get; set; } // String name of weapon
    public string Flair { get; set; } // Extra text above weapon
    public string BulletHoleSize { get; set; } // Size of bullet hole called by PoolManager
    public int ReserveAmmo { get; set; } // Amount of reserve ammo weapon spawns with
    public int MaxReserveAmmo { get; set; } // Maximum capacity of reserve ammo
    public int CurrentMag { get; set; } // Amount of magazine ammo weapon spawns with
    public bool ClipFed { get; set; } // Can weapon be reloaded in 1 cycle?
    public float ChamberTime { get; set; } // Duration it takes before a non-clip-fed reload occurs
    public int MaxMag { get; set; } // Maximum capacity of magazine
    public float DrawTime { get; set; } // Speed weapon is switched to
    public float BulletDamage { get; set; } // Base damage of each bullet
    public float DamageFalloff { get; set; } // % damage falls off at max range
    public float ReloadSpeed { get; set; } // Speed weapon is reloaded at
    public float FiringRate { get; set; } // Amount of time inbetween shots
    public int PointValue { get; set; } // Base points given when hitting a zombie
    public float PointMultiplier { get; set; } // Point multiplier when hitting a headshot
    public float HeadshotMultiplier { get; set; } // Damage multiplier when hitting a headshot
    public bool Pierce { get; set; } // Can weapon do collateral damage?
    public float MaxRange { get; set; } // Max range weapon can shoot
    public bool Automatic { get; set; } // Can weapon be fired fully automatic?
    public int Projectiles { get; set; } // How many projectiles are shot in one click
    public float BulletSpreadRadius { get; set; } // Radius of inaccuracy
    public float ExplosionRadius { get; set; } // Explosion radius of hit.point
    public float ZoomValue { get; set; } // FOV while zoomed in (60 is default)
    public float ZoomMoveSpeed { get; set; } // Fraction of moving speed while zoomed in

}


