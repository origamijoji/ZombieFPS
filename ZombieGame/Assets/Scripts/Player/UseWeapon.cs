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
    private Points playerPoints;

    public float firingTime;

    public float reloadTime;
    public bool isReloading;
    public bool isSwitching;

    public GameObject noAmmoText;

    private void Awake() {
        mouseLook = gameObject.GetComponent<MouseLook>();
        playerCamera = mouseLook.playerCamera;
        poolManager = PoolManager.instance;
        playerPoints = gameObject.GetComponent<Points>();
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
        equippedI = currentWeapon.WeaponName;
        primaryI = primaryWeapon.WeaponName;
        secondaryI = secondaryWeapon.WeaponName;
        currentMagazineI = currentWeapon.CurrentMag;
        reserveAmmoI = currentWeapon.ReserveAmmo;
    }
    private void ProcessReload() {
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentWeapon.ReserveAmmo > 0 && currentWeapon.CurrentMag != currentWeapon.MaxMag) {
            isReloading = true;
            StartCoroutine(Reload());
        }
        else if (currentWeapon.CurrentMag == 0 && !isReloading && currentWeapon.ReserveAmmo > 0) {
            isReloading = true;
            StartCoroutine(Reload());
        }
    }


    IEnumerator Reload() {
        while(isReloading) {
            yield return new WaitForSeconds(currentWeapon.ReloadSpeed);
            int ammoRequired = currentWeapon.MaxMag - currentWeapon.CurrentMag;
            if(currentWeapon.ReserveAmmo > ammoRequired) {
                currentWeapon.CurrentMag = currentWeapon.MaxMag;
                currentWeapon.ReserveAmmo -= ammoRequired;
                isReloading = false;
            }
            else {
                currentWeapon.CurrentMag += currentWeapon.ReserveAmmo;
                currentWeapon.ReserveAmmo = 0;
                isReloading = false;
            }
        }
        yield break;
    }


    private void Fire() {
        if (firingTime > 0) firingTime -= Time.deltaTime;
        if (Input.GetMouseButton(0) && !isReloading && firingTime <= 0 && currentWeapon.CurrentMag > 0) {
            currentWeapon.CurrentMag--;
            firingTime = currentWeapon.FiringRate;
            ZombieHealth hitZombie;
            if (Physics.Raycast(playerCamera.gameObject.transform.position, playerCamera.gameObject.transform.forward, out RaycastHit hit, Mathf.Infinity, playerMask)) {
                if (hit.transform.gameObject.CompareTag("Zombie")) {
                    hitZombie = hit.transform.gameObject.GetComponent<ZombieHealth>();
                    hitZombie.health -= currentWeapon.BulletDamage;
                    playerPoints.AddPoints(currentWeapon.PointValue);
                    
                }
                else {
                    Quaternion rot = Quaternion.FromToRotation(Vector3.forward, hit.normal);
                    poolManager.SpawnFromPool(currentWeapon.BulletHoleSize, hit.point, rot);
                }
            }
        }
    }

    private void SwitchWeapon() {
        if (Input.GetKeyDown(KeyCode.Alpha1) && currentWeapon == secondaryWeapon && !isReloading) {
            StartCoroutine(SwitchToPrimary());
        } else if (Input.GetKeyDown(KeyCode.Alpha2) && currentWeapon == primaryWeapon && !isReloading) {
            StartCoroutine(SwitchToSecondary());
        }
    }


    IEnumerator SwitchToPrimary() {
        yield return new WaitForSeconds(primaryWeapon.DrawTime);
        currentWeapon = primaryWeapon;
    }
    IEnumerator SwitchToSecondary() {
        yield return new WaitForSeconds(secondaryWeapon.DrawTime);
        currentWeapon = secondaryWeapon;
    }

    private void NoAmmo() {
        if (currentWeapon.ReserveAmmo == 0 && currentWeapon.CurrentMag == 0) {
            noAmmoText.SetActive(true);
        }
        else { noAmmoText.SetActive(false); }
    }
}
