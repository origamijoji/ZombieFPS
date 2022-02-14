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
        if (useWeapon.CanPurchase()) {
            physUI.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E)) {
                StartCoroutine(GiveGunToPlayer());
            }
        }
    }

    IEnumerator GiveGunToPlayer() {
        useWeapon.isInteracting = true;
        while(Input.GetKey(KeyCode.E) && useWeapon.CanPurchase()) {
            if (useWeapon.secondaryWeapon.WeaponName != weapon && useWeapon.primaryWeapon.WeaponName != weapon) { /// if weapon is not currently owned
                playerPoints.RemovePoints(cost);
                if (useWeapon.secondaryWeapon is None) { //if no current secondary, make weapon secondary
                    useWeapon.interactTimer = useWeapon.interactTime;
                    yield return new WaitForSeconds(useWeapon.interactTime);
                    BuySecondWeapon();
                }
                else { //if player has two weapons, make their current weapon the purchassed weapon
                    useWeapon.interactTimer = useWeapon.interactTime;
                    yield return new WaitForSeconds(useWeapon.interactTime);
                    ReplaceWeapon();
                }
            }
            else { ///if weapon is obtained, purchase ammo instead
                playerPoints.RemovePoints(ammoCost);
                if (useWeapon.primaryWeapon.WeaponName == weapon) {
                    useWeapon.interactTimer = useWeapon.interactTime;
                    yield return new WaitForSeconds(useWeapon.interactTime);
                    BuyAmmo();
                }
            }
            yield return null;
        }
        yield break;
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
