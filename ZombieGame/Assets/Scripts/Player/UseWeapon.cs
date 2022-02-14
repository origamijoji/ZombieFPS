using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseWeapon : MonoBehaviour {
    public Weapon primaryWeapon;
    public Weapon secondaryWeapon;

    public string primary;
    public string secondary;
    public int currentMagazineI;
    public int reserveAmmoI;


    public LayerMask playerMask;
    private MouseLook mouseLook;
    private Camera playerCamera;
    private PoolManager poolManager;
    private Points playerPoints;

    public float firingTime;

    public bool isReloading;
    public float reloadTimer;

    public bool isSwitching;
    public float switchTimer;

    public bool isInteracting;
    public float interactTimer;
    public float interactTime;

    public float buyRange;
    public bool isLookingAtBuyZone;

    //automatic
    //pierce
    //range
    public GameObject noAmmoText;

    private void Awake() {
        //local components
        mouseLook = gameObject.GetComponent<MouseLook>();
        playerCamera = mouseLook.playerCamera;
        playerPoints = gameObject.GetComponent<Points>();
        //global components
        poolManager = PoolManager.instance;
    }

    private void Start() {
        playerMask = ~playerMask;
        primaryWeapon = new Pistol();
        secondaryWeapon = new None();
    }

    private void Update() {
        ProcessReload();
        Fire();
        SwitchWeapon();
        ShowInspector();
        NoAmmo();
    }

    private void ShowInspector() {
        primary = primaryWeapon.WeaponName;
        secondary = secondaryWeapon.WeaponName;
        currentMagazineI = primaryWeapon.CurrentMag;
        reserveAmmoI = primaryWeapon.ReserveAmmo;
    }
    private void ProcessReload() {
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && !isSwitching && primaryWeapon.CurrentMag != primaryWeapon.MaxMag && primaryWeapon.ReserveAmmo > 0 ) {
            StartCoroutine(Reload());
        }
        else if (primaryWeapon.CurrentMag == 0 && !isReloading && !isSwitching && primaryWeapon.ReserveAmmo > 0) {
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload() {
        isReloading = true;
        while (isReloading) {

            for (float reloadingTime = primaryWeapon.ReloadSpeed; reloadingTime > 0; reloadTimer -= Time.deltaTime) {
                reloadTimer = reloadingTime;
            }
            int ammoRequired = primaryWeapon.MaxMag - primaryWeapon.CurrentMag;
            if (primaryWeapon.ReserveAmmo > ammoRequired) {
                primaryWeapon.CurrentMag = primaryWeapon.MaxMag;
                primaryWeapon.ReserveAmmo -= ammoRequired;
                isReloading = false;
            }
            else {
                primaryWeapon.CurrentMag += primaryWeapon.ReserveAmmo;
                primaryWeapon.ReserveAmmo = 0;
                isReloading = false;
            }
        }
        yield break;
    }
    private void SwitchWeapon() {
        if (Input.GetKeyDown(KeyCode.Q) && !isReloading) {
            StartCoroutine(Switch());
        }
    }

    public IEnumerator Switch() {
        isSwitching = true;
        switchTimer = secondaryWeapon.DrawTime;
        yield return new WaitForSeconds(secondaryWeapon.DrawTime);
        Weapon temp = secondaryWeapon;
        secondaryWeapon = primaryWeapon;
        primaryWeapon = temp;
        isSwitching = false;

    }

    #region Firing

    private void Fire() {
        if (firingTime > 0) firingTime -= Time.deltaTime;
        if (Input.GetMouseButton(0) && !isReloading && !isSwitching && firingTime <= 0 && primaryWeapon.CurrentMag > 0) {
            primaryWeapon.CurrentMag--;
            firingTime = primaryWeapon.FiringRate;
            ZombieHealth hitZombie;
            if (Physics.Raycast(playerCamera.gameObject.transform.position, playerCamera.gameObject.transform.forward, out RaycastHit hit, Mathf.Infinity, playerMask)) {
                if (hit.transform.gameObject.CompareTag("Zombie Head")) {
                    hitZombie = hit.collider.gameObject.GetComponentInParent<ZombieHealth>();
                    hitZombie.TakeDamage(primaryWeapon.BulletDamage, primaryWeapon.Crit);
                    playerPoints.AddPoints(primaryWeapon.PointValue);
                }
                else if (hit.transform.gameObject.CompareTag("Zombie")) {
                    hitZombie = hit.collider.gameObject.GetComponentInParent<ZombieHealth>();
                    hitZombie.TakeDamage(primaryWeapon.BulletDamage);
                    playerPoints.AddPoints(primaryWeapon.PointValue);
                }
                else {
                    Quaternion rot = Quaternion.FromToRotation(Vector3.forward, hit.normal);
                    poolManager.SpawnFromPool(primaryWeapon.BulletHoleSize, hit.point, rot);
                }
            }
        }
    }
    #endregion
    #region NoAmmo

    private void NoAmmo() {
        if (primaryWeapon.ReserveAmmo == 0 && primaryWeapon.CurrentMag == 0) {
            noAmmoText.SetActive(true);
        }
        else { noAmmoText.SetActive(false); }
    }
    #endregion
    #region CanPurchase

    public bool CanPurchase() {
        if (Physics.Raycast(playerCamera.gameObject.transform.position, playerCamera.gameObject.transform.forward, out RaycastHit hit, buyRange, 9)
            && !isReloading && !isSwitching && !isInteracting) {
            return true;
        }
        else { return false; }
    }
    #endregion
}
