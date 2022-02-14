using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GiveGun : MonoBehaviour {
    public int cost;
    public int ammoCost;
    public string weapon;

    private Type weaponType;
    private GameObject player;
    private UseWeapon useWeapon;
    private Points playerPoints;
    private PhysicalUI physUI;
    private bool isInRange;

    private void Awake() {
        //player components
        player = GameObject.FindGameObjectWithTag("Player");
        useWeapon = player.GetComponent<UseWeapon>();
        playerPoints = player.GetComponent<Points>();
        //local components
        physUI = gameObject.GetComponentInChildren<PhysicalUI>();
    }

    void Start() {
        weaponType = Type.GetType(weapon);
    }
    private void Update() {
        if (Vector3.Distance(gameObject.transform.position, player.gameObject.transform.position) < useWeapon.buyRange) {
            physUI.gameObject.SetActive(true);
        }
        else { physUI.gameObject.SetActive(false); }
    }

    public void DecidePurchase() {
        if (useWeapon.secondaryWeapon.WeaponName != weapon && useWeapon.primaryWeapon.WeaponName != weapon) { /// if weapon is not currently owned
            playerPoints.RemovePoints(cost);
            if (useWeapon.secondaryWeapon is None) { //if no current secondary, make weapon secondary
                BuySecondWeapon();
            }
            else { //if player has two weapons, make their current weapon the purchassed weapon
                ReplaceWeapon();
            }
        }
        else { ///if weapon is obtained, purchase ammo instead
            playerPoints.RemovePoints(ammoCost);
            if (useWeapon.primaryWeapon.WeaponName == weapon) {
                BuyAmmo();
            }
            else { //if weapon is not in main hand, do nothing
                return;
            }
        }
    }

    private void BuySecondWeapon() {
        useWeapon.secondaryWeapon = (Weapon)Activator.CreateInstance(weaponType);
        useWeapon.isInteracting = false;
    }
    private void ReplaceWeapon() {
        useWeapon.primaryWeapon = (Weapon)Activator.CreateInstance(weaponType);
        useWeapon.isInteracting = false;
    }
    private void BuyAmmo() {
        useWeapon.primaryWeapon.ReserveAmmo = useWeapon.primaryWeapon.MaxReserveAmmo;
        useWeapon.isInteracting = false;
    }

}
