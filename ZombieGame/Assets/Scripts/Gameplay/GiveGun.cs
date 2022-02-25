using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GiveGun : MonoBehaviour {

    /// <summary>
    /// This script lets you edit its values in the inspector, and the player will call them when they purchase a weapon
    /// The UI of the script will activate only when the player is within 3 units of it.
    /// </summary>

    [Tooltip("Name of weapon being sold")]
    public string weapon;
    [Header ("~ Cost ~")]
    [Tooltip("Cost of currently sold weapon")]
    public int cost;
    [Tooltip("cost of ammo for the currently sold weapon")]
    public int ammoCost;


    [HideInInspector]
    public Type weaponType;
    private GameObject player;
    private GameObject UI;

    [Header("~ Stats ~")]
    public float RPM;
    public int magSize;
    public int reserveAmmo;
    public float damage;
    public float critValue;
    public string weight;

    public Weapon thisWeapon;

    private void OnValidate() {
        if(Type.GetType(weapon) != null) {
            weaponType = Type.GetType(weapon);
        }
        thisWeapon = (Weapon)Activator.CreateInstance(weaponType);
        RPM = Mathf.Round(1 / thisWeapon.FiringRate * 60);
        magSize = thisWeapon.MaxMag;
        reserveAmmo = thisWeapon.ReserveAmmo;
        damage = thisWeapon.BulletDamage;
        critValue = thisWeapon.HeadshotMultiplier;
    }

    private void Awake() {
        //player components
        player = GameObject.FindGameObjectWithTag("Player");
        //local components
        UI = gameObject.GetComponentInChildren<Canvas>().gameObject;
    }

    void Start() {
        OnValidate();
    }

    private void Update() {

        if (Vector3.Distance(gameObject.transform.position, player.gameObject.transform.position) < 3) {
            UI.SetActive(true);
        }
        else { UI.SetActive(false); }
    }
}


