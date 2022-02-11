using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseWeapon : MonoBehaviour
{
    public Weapon currentWeapon;
    public Weapon primaryWeapon;
    public Weapon secondaryWeapon;

    public string equippedI;
    public string primaryI;
    public string secondaryI;
    public int currentMagazineI;
    public int reserveAmmoI;


    public LayerMask playerMask;
    private MouseLook mouseLook;
    private Camera playerCamera;
    private PoolManager poolManager;

    public float firingTime;

    public float reloadTime;
    public bool isReloading;
    public bool isSwitching;

    public GameObject noAmmoText;

    private void Awake() {
        mouseLook = gameObject.GetComponent<MouseLook>();
        playerCamera = mouseLook.playerCamera;
        poolManager = PoolManager.instance;
    }

    private void Start() {
        playerMask = ~playerMask;
        primaryWeapon = new Pistol();
        secondaryWeapon = new None();
        currentWeapon = primaryWeapon;
    }

    private void Update() {
        ProcessReload();
        Fire();
        SwitchWeapon();
        ShowInspector();
        NoAmmo();
    }

    private void ShowInspector() {
        equippedI = currentWeapon.weaponName;
        primaryI = primaryWeapon.weaponName;
        secondaryI = secondaryWeapon.weaponName;
        currentMagazineI = currentWeapon.currentMag;
        reserveAmmoI = currentWeapon.reserveAmmo;
    }
    private void ProcessReload() {
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentWeapon.reserveAmmo > 0 && currentWeapon.currentMag != currentWeapon.maxMag) {
            isReloading = true;
            StartCoroutine(Reload());
        }
        else if (currentWeapon.currentMag == 0 && !isReloading && currentWeapon.reserveAmmo > 0) {
            isReloading = true;
            StartCoroutine(Reload());
        }
    }


    IEnumerator Reload() {
        while(isReloading) {
            yield return new WaitForSeconds(currentWeapon.reloadSpeed);
            int ammoRequired = currentWeapon.maxMag - currentWeapon.currentMag;
            if(currentWeapon.reserveAmmo > ammoRequired) {
                currentWeapon.currentMag = currentWeapon.maxMag;
                currentWeapon.reserveAmmo -= ammoRequired;
                isReloading = false;
            }
            else {
                currentWeapon.currentMag += currentWeapon.reserveAmmo;
                currentWeapon.reserveAmmo = 0;
                isReloading = false;
            }
        }
        yield break;
    }


    private void Fire() {
        if (firingTime > 0) firingTime -= Time.deltaTime;
        if (Input.GetMouseButton(0) && !isReloading && firingTime <= 0 && currentWeapon.currentMag > 0) {
            currentWeapon.currentMag--;
            firingTime = currentWeapon.firingRate;
            ZombieHealth hitZombie;
            if (Physics.Raycast(playerCamera.gameObject.transform.position, playerCamera.gameObject.transform.forward, out RaycastHit hit, Mathf.Infinity, playerMask)) {
                if (hit.transform.gameObject.CompareTag("Zombie")) {
                    hitZombie = hit.transform.gameObject.GetComponent<ZombieHealth>();
                    hitZombie.health -= currentWeapon.bulletDamage;
                }
                else {
                    Quaternion rot = Quaternion.FromToRotation(Vector3.forward, hit.normal);
                    poolManager.SpawnFromPool("Bullet Hole", hit.point, rot);
                }
            }
        }
    }

    private void SwitchWeapon() {
        if (Input.GetKeyDown(KeyCode.Alpha1) && currentWeapon == secondaryWeapon && !isReloading) {
            StartCoroutine(SwitchToPrimary());
        } else if (Input.GetKeyDown(KeyCode.Alpha2) && currentWeapon == primaryWeapon && !isReloading && secondaryWeapon.name != "None") {
            StartCoroutine(SwitchToSecondary());
        }
    }


    IEnumerator SwitchToPrimary() {
        yield return new WaitForSeconds(primaryWeapon.drawTime);
        currentWeapon = primaryWeapon;
    }
    IEnumerator SwitchToSecondary() {
        yield return new WaitForSeconds(secondaryWeapon.drawTime);
        currentWeapon = secondaryWeapon;
    }

    public void PickupGun(Weapon newGun) {
        newGun.GetType();
        if(secondaryWeapon.name == "None") {
            secondaryWeapon = newGun;
            SwitchToSecondary();
        }
        else if(currentWeapon == primaryWeapon) {
            primaryWeapon = newGun;
        }
        else if(currentWeapon == secondaryWeapon) {
            secondaryWeapon = newGun;
        }
    }

    private void NoAmmo() {
        if (currentWeapon.reserveAmmo == 0 && currentWeapon.currentMag == 0) {
            noAmmoText.SetActive(true);
        }
        else { noAmmoText.SetActive(false); }
    }
}
