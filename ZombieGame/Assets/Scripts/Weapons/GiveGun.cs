using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GiveGun : MonoBehaviour {
    public int cost;
    public float range = 3;
    public string weapon;

    private Type weaponType;
    private GameObject player;
    private UseWeapon playerUseWeapon;
    private Points playerPoints;
    public PhysicalUI physUI;

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
        playerPoints.RemovePoints(cost);
        if (playerUseWeapon.secondaryWeapon is None) {
            playerUseWeapon.secondaryWeapon = (Weapon)Activator.CreateInstance(weaponType);
        }
        else {
            playerUseWeapon.currentWeapon = (Weapon)Activator.CreateInstance(weaponType);
        }
    }
}
