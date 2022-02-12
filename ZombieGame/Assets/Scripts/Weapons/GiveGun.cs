using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GiveGun : MonoBehaviour {
    public int cost;
    public int ammoCost;
    public float range = 3;
    public string weapon;

    private Type weaponType;
    private GameObject player;
    private UseWeapon playerUseWeapon;
    private Points playerPoints;
    private PhysicalUI physUI;
    public HUDManager hudManager;

    void Start() {
        weaponType = Type.GetType(weapon);
        player = GameObject.FindGameObjectWithTag("Player");
        playerUseWeapon = player.GetComponent<UseWeapon>();
        playerPoints = player.GetComponent<Points>();
        physUI = gameObject.GetComponentInChildren<PhysicalUI>();
        hudManager = GameObject.Find("HUD Manager").GetComponent<HUDManager>();
    }
    private void Update() {
        if (Vector3.Distance(player.transform.position, transform.position) < range) {
            physUI.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E)) {
                PickupGun();
            }
        }
        else { physUI.gameObject.SetActive(false); }
    }

    public void PickupGun() {
        //if gun not obtained, purchase it
        if (playerUseWeapon.secondaryWeapon.WeaponName != weapon && playerUseWeapon.primaryWeapon.WeaponName != weapon) {
            playerPoints.RemovePoints(cost);
            if (playerUseWeapon.secondaryWeapon is None) {
                playerUseWeapon.secondaryWeapon = (Weapon)Activator.CreateInstance(weaponType);
                hudManager.switchTimer = playerUseWeapon.secondaryWeapon.DrawTime;
                StartCoroutine(playerUseWeapon.Switch());
            }
            else {
               playerUseWeapon.primaryWeapon = (Weapon)Activator.CreateInstance(weaponType);
            }
        }
        //if weapon is obtained, purchase ammo instead
        else {
            playerPoints.RemovePoints(ammoCost);
            if(playerUseWeapon.primaryWeapon.WeaponName == weapon) {
                playerUseWeapon.primaryWeapon = (Weapon)Activator.CreateInstance(weaponType);
            }
        }
    }
}
