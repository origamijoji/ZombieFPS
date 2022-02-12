using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseWeapon : MonoBehaviour
{
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
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && !isSwitching && primaryWeapon.ReserveAmmo > 0 && primaryWeapon.CurrentMag != primaryWeapon.MaxMag) {
            isReloading = true;
            StartCoroutine(Reload());
        }
        else if (primaryWeapon.CurrentMag == 0 && !isReloading && !isSwitching && primaryWeapon.ReserveAmmo > 0) {
            isReloading = true;
            StartCoroutine(Reload());
        }
    }


    IEnumerator Reload() {
        while(isReloading) {
            yield return new WaitForSeconds(primaryWeapon.ReloadSpeed);
            int ammoRequired = primaryWeapon.MaxMag - primaryWeapon.CurrentMag;
            if(primaryWeapon.ReserveAmmo > ammoRequired) {
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


    private void Fire() {
        if (firingTime > 0) firingTime -= Time.deltaTime;
        if (Input.GetMouseButton(0) && !isReloading && !isSwitching && firingTime <= 0 && primaryWeapon.CurrentMag > 0) {
            primaryWeapon.CurrentMag--;
            firingTime = primaryWeapon.FiringRate;
            ZombieHealth hitZombie;
            if (Physics.Raycast(playerCamera.gameObject.transform.position, playerCamera.gameObject.transform.forward, out RaycastHit hit, Mathf.Infinity, playerMask)) {
                if (hit.transform.gameObject.CompareTag("Zombie")) {
                    hitZombie = hit.transform.gameObject.GetComponent<ZombieHealth>();
                    hitZombie.health -= primaryWeapon.BulletDamage;
                    playerPoints.AddPoints(primaryWeapon.PointValue);
                    
                }
                else {
                    Quaternion rot = Quaternion.FromToRotation(Vector3.forward, hit.normal);
                    poolManager.SpawnFromPool(primaryWeapon.BulletHoleSize, hit.point, rot);
                }
            }
        }
    }

    private void SwitchWeapon() {
        if (Input.GetKeyDown(KeyCode.Q) && !isReloading) {
            StartCoroutine(Switch());
        } 
    }


    public IEnumerator Switch() {
        isSwitching = true;
        yield return new WaitForSeconds(secondaryWeapon.DrawTime);
        Weapon temp = secondaryWeapon;
        secondaryWeapon = primaryWeapon;
        primaryWeapon = temp;
        isSwitching = false;

    }

    private void NoAmmo() {
        if (primaryWeapon.ReserveAmmo == 0 && primaryWeapon.CurrentMag == 0) {
            noAmmoText.SetActive(true);
        }
        else { noAmmoText.SetActive(false); }
    }

}
