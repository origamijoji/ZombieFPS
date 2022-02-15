using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UseWeapon : MonoBehaviour {

    /// <summary>
    /// Handles all actions a player may do using a weapon
    ///
    /// For multiplayer implementation: The UI will have to be channged to a child of the player obj, because this system relies on only one existing 
    /// or setting referenced UI to each individual player manually.
    /// </summary>


    #region Inspector Var
    public string primary;
    public string secondary;
    public int currentMagazine;
    public int reserveAmmo;
    #endregion

    [HideInInspector]
    public Weapon primaryWeapon;
    [HideInInspector]
    public Weapon secondaryWeapon;

    public LayerMask playerMask;
    private MouseLook mouseLook;
    private Camera playerCamera;
    private PoolManager poolManager;
    private Points points;
    private MovePlayer movePlayer;

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
    private GiveGun buyZone;
    public LayerMask whatIsBuyZone;

    Coroutine PurchaseWeapon;
    //automatic
    //pierce
    //range
    public GameObject noAmmoText;
    #region MonoBehaviour Awake(), Start()

    private void Awake() {
        //local components
        movePlayer = gameObject.GetComponent<MovePlayer>();
        mouseLook = gameObject.GetComponent<MouseLook>();
        playerCamera = mouseLook.playerCamera;
        points = gameObject.GetComponent<Points>();

        //global components
        poolManager = PoolManager.instance;
    }

    private void Start() {
        playerMask = ~playerMask;
        primaryWeapon = new Pistol();
        secondaryWeapon = new None();
    }
    #endregion
    #region MonoBehaviour Update()
    private void Update() {
        ProcessReload();
        Fire();
        SwitchWeapon();
        ShowInspector();
        NoAmmo();
        DoPurchase();

        if (isInteracting) {
            mouseLook.LockMouseInput(true);
            movePlayer.LockMovement(true);
        }
        else {
            mouseLook.LockMouseInput(false);
            movePlayer.LockMovement(false);
        }
    }
    #endregion
    #region Inspector
    /*
     * This class has its own primary and secondary fields to store the currently used weapons names and ammo amounts (for ease of access)
     * 
     * DO NOT GET THESE CONFUSED WITH THE PRIMARY/SECONDARY WEAPON CLASSES VARIABLES OF SIMILAR NAMES
     */
    private void ShowInspector() {
        primary = primaryWeapon.WeaponName;
        secondary = secondaryWeapon.WeaponName;
        currentMagazine = primaryWeapon.CurrentMag;
        reserveAmmo = primaryWeapon.ReserveAmmo;
    }
    #endregion
    #region Reload
    /*
     * If player has ammo, is free from actions, doesn't currently have a full magazine, and has reserve ammo start a Reload
     * Get the required amount of ammo needed in the magazine to be at max
     * Set the magazine to the max ammount
     * Subtract the required amount from the reserve ammo
     * If the required amount of ammo is not owned
     * Add all reserve ammo to the magazine, set reserve ammo to 0
     */

    private void ProcessReload() {
        if (Input.GetKeyDown(KeyCode.R) && CanOperate() && primaryWeapon.CurrentMag != primaryWeapon.MaxMag && primaryWeapon.ReserveAmmo > 0) {
            StartCoroutine(Reload());
        }
        else if (primaryWeapon.CurrentMag == 0 && CanOperate() && primaryWeapon.ReserveAmmo > 0) {
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload() {
        isReloading = true;
        while (isReloading) {
            reloadTimer = primaryWeapon.ReloadSpeed;
            yield return new WaitForSeconds(primaryWeapon.ReloadSpeed);
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
    #endregion
    #region Switch
    /*
     * If player is free from any actions and presses Q (and they possess a second weapon)...
     * Store their primary weapon in a temporary obj
     * Make their secondary weapon their primary
     * Make their primary weapon their secondary
     */

    private void SwitchWeapon() {
        if (Input.GetKeyDown(KeyCode.Q) && CanOperate() && !(secondaryWeapon is None)) {
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
        yield break;
    }
    #endregion
    #region Firing
    /* 
     * Player will shoot a raycast with dist specified by the current weapon (primary weapon) and returns the first hit collider, 
     * If the collider is layered a zombie or head, call the damage method on the hit zombie...
     * Otherwise, place a bullet hole decal on the obj specified by the primaryWeapon's bullet hole size.
     */
    private void Fire() { // LINEAR FIRING
        if (firingTime > 0) firingTime -= Time.deltaTime;
        if (Input.GetMouseButton(0) && CanOperate() && firingTime <= 0 && primaryWeapon.CurrentMag > 0) { // FULL AUTO
            primaryWeapon.CurrentMag--;
            firingTime = primaryWeapon.FiringRate;
            ZombieHealth hitZombie;
            if (Physics.Raycast(playerCamera.gameObject.transform.position, playerCamera.gameObject.transform.forward, out RaycastHit hit, primaryWeapon.MaxRange, playerMask)) {
                if (hit.transform.gameObject.CompareTag("Zombie Head")) {
                    hitZombie = hit.collider.gameObject.GetComponentInParent<ZombieHealth>();
                    hitZombie.TakeDamage(primaryWeapon.BulletDamage, primaryWeapon.HeadshotMultiplier);
                    points.AddPoints(primaryWeapon.PointValue, primaryWeapon.PointMultiplier);
                }
                else if (hit.transform.gameObject.CompareTag("Zombie")) {
                    hitZombie = hit.collider.gameObject.GetComponentInParent<ZombieHealth>();
                    hitZombie.TakeDamage(primaryWeapon.BulletDamage);
                    points.AddPoints(primaryWeapon.PointValue);
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
    /*
     * If player has no ammo in their current weapon, display UI text.
     */
    private void NoAmmo() {
        if (primaryWeapon.ReserveAmmo == 0 && primaryWeapon.CurrentMag == 0) {
            noAmmoText.SetActive(true);
        }
        else { noAmmoText.SetActive(false); }
    }
    #endregion
    #region Return Methods
    public bool CanPurchase() { //if player is looking at a buy zone, return true
        if (Physics.Raycast(playerCamera.gameObject.transform.position, playerCamera.gameObject.transform.forward, buyRange, whatIsBuyZone)) {
            return true;
        }
        else { return false; }
    }

    public bool CanOperate() { //if player is not currently in an action, return true
        if (isReloading || isInteracting || isSwitching) {
            return false;
        }
        else { return true; }
    }
    #endregion
    #region Weapon Buying
    /*
     * If player is looking at a buy zone and free from any actions, start a purchase
     * Get the current buy zone they are looking at
     * If player is still looking at buy zone (their sensitivity will be locked) and holding E for seconds specified by interactTime...
     * The player will either get a secondary, replace their current gun, or buy ammo
     */

    private void DoPurchase() {
        if (CanPurchase() && CanOperate() && Input.GetKeyDown(KeyCode.E)) {
            isInteracting = true;
            Physics.Raycast(playerCamera.gameObject.transform.position, playerCamera.gameObject.transform.forward, out RaycastHit hit, buyRange, whatIsBuyZone);
            buyZone = hit.transform.gameObject.GetComponent<GiveGun>();
            //StartCoroutine(Purchasing());
            if (Purchasing() != null) {
                PurchaseWeapon = StartCoroutine(Purchasing());
            }
        }

        if (Input.GetKeyUp(KeyCode.E)) {
            StopCoroutine(PurchaseWeapon);
            isInteracting = false;
        }
    }

    IEnumerator Purchasing() {
        Debug.Log("purchasing called");
        while (CanPurchase() && isInteracting) {
            Debug.Log("purchasing");
            interactTimer = interactTime;
            yield return new WaitForSeconds(interactTime);
            DecidePurchase();
            isInteracting = false;
            yield break;
        }
        isInteracting = false;
        yield break;
    }

    private void DecidePurchase() {
        Debug.Log(secondaryWeapon.WeaponName);
        Debug.Log(buyZone.weapon);
        if (!secondaryWeapon.WeaponName.Equals(buyZone.weapon) && !primaryWeapon.WeaponName.Equals(buyZone.weapon)) { // if weapon is not currently owned
            points.RemovePoints(buyZone.cost);
            if (secondaryWeapon is None) { //if no current secondary, make weapon secondary then switch weapons
                Debug.Log("Secondary Purchased");
                secondaryWeapon = (Weapon)Activator.CreateInstance(buyZone.weaponType);
                Weapon temp = secondaryWeapon;
                secondaryWeapon = primaryWeapon;
                primaryWeapon = temp;
            }
            else { //if player has two weapons, make their current weapon the purchassed weapon
                Debug.Log("Primary Purchased");
                primaryWeapon = (Weapon)Activator.CreateInstance(buyZone.weaponType);
            }
        }
        else { //if weapon is obtained, purchase ammo instead
            points.RemovePoints(buyZone.ammoCost);
            if (primaryWeapon.WeaponName == buyZone.weapon) {
                Debug.Log("Ammo Purchased");
                primaryWeapon.ReserveAmmo = primaryWeapon.MaxReserveAmmo;
            }
            else { //if weapon is not in main hand, do nothing
                return;
            }
        }
    }
    #endregion
    private void UpdateWeaponStats() {

    }
}
