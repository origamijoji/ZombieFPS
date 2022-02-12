using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GiveGun : MonoBehaviour
{
    public int cost;
    public float range;
    string typeString = "Fireball";
    public Type weaponType;

    private Weapon newGun;
    private GameObject player;
    private UseWeapon playerUseWeapon;
    private Points playerPoints;

    void Start()
    {
        weaponType = Type.GetType(typeString);
        newGun = (Weapon) Activator.CreateInstance(weaponType);
        player = GameObject.FindGameObjectWithTag("Player");

        playerUseWeapon = player.GetComponent<UseWeapon>();
        playerPoints = player.GetComponent<Points>();
    }

    private void OnTriggerEnter(Collider other) {
        PickupGun();

    }

    public void PickupGun() {
        //playerPoints.currentPoints -= cost;
        if (playerUseWeapon.secondaryWeapon is None) {
            playerUseWeapon.secondaryWeapon = newGun;
        } 
        else {
            playerUseWeapon.currentWeapon = newGun;
        }
    }
}
