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

    void Start() {
        weaponType = Type.GetType(weapon);
        player = GameObject.FindGameObjectWithTag("Player");
        playerUseWeapon = player.GetComponent<UseWeapon>();
        playerPoints = player.GetComponent<Points>();
        physUI = gameObject.GetComponentInChildren<PhysicalUI>();
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
                playerUseWeapon.otherWeapon = playerUseWeapon.secondaryWeapon;
                StartCoroutine(playerUseWeapon.SwitchToSecondary());
            }
            else {
                if (playerUseWeapon.currentWeapon == playerUseWeapon.primaryWeapon) {
                    playerUseWeapon.primaryWeapon = (Weapon)Activator.CreateInstance(weaponType);
                    playerUseWeapon.currentWeapon = playerUseWeapon.primaryWeapon;
                }
                else {
                    playerUseWeapon.secondaryWeapon = (Weapon)Activator.CreateInstance(weaponType);
                    playerUseWeapon.currentWeapon = playerUseWeapon.secondaryWeapon;
                }
            }
        }
        //if weapon is obtained, purchase ammo instead
        else {
            playerPoints.RemovePoints(ammoCost);
            if(playerUseWeapon.primaryWeapon.WeaponName == weapon) {
                playerUseWeapon.primaryWeapon = (Weapon)Activator.CreateInstance(weaponType);
            }
            else if(playerUseWeapon.secondaryWeapon.WeaponName == weapon) {
                playerUseWeapon.secondaryWeapon = (Weapon)Activator.CreateInstance(weaponType);
            }

        }
    }
}
