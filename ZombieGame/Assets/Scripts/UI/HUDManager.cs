using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {
    private GameObject player;
    private UseWeapon useWeapon;
    private Points playerPoints;

    [SerializeField] private TextMeshProUGUI gunText;
    [SerializeField] private TextMeshProUGUI magText;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI pointText;
    [SerializeField] private TextMeshProUGUI transactionText;
    [SerializeField] private TextMeshProUGUI purchaseText;

    [SerializeField] private GameObject reloading;
    [HideInInspector] public float reloadTimer;
    private Slider reloadingSlider;

    [SerializeField] private GameObject switching;
    [HideInInspector] public float switchTimer;
    private Slider switchingSlider;

    [SerializeField] private GameObject interacting;
    [HideInInspector] public float interactTimer;
    private Slider interactingSlider;


    private void Awake() {
        //player components
        player = GameObject.FindGameObjectWithTag("Player");
        useWeapon = player.GetComponent<UseWeapon>();
        playerPoints = player.GetComponent<Points>();
        //local components
        reloadingSlider = reloading.GetComponentInChildren<Slider>();
        switchingSlider = switching.GetComponentInChildren<Slider>();
        interactingSlider = interacting.GetComponentInChildren<Slider>();
    }

    void Update() {
        gunText.text = useWeapon.primaryWeapon.WeaponName;
        magText.text = useWeapon.primaryWeapon.CurrentMag.ToString();
        ammoText.text = useWeapon.primaryWeapon.ReserveAmmo.ToString();
        pointText.text = playerPoints.currentPoints.ToString();
        transactionText.text = playerPoints.lastTransaction;

        if (useWeapon.reloadTimer > 0) {
            reloading.SetActive(true);
            reloadingSlider.maxValue = useWeapon.primaryWeapon.ReloadSpeed;
            reloadingSlider.value = reloadTimer;
            reloadTimer -= Time.deltaTime;
        }
        else {
            reloading.SetActive(false);
        }

        if (useWeapon.switchTimer > 0) {
            switching.SetActive(true);
            switchingSlider.maxValue = useWeapon.secondaryWeapon.DrawTime;
            switchingSlider.value = switchTimer;
            switchTimer -= Time.deltaTime;
        }
        else {
            switching.SetActive(false);
        }

        if(interactTimer > 0) {
            interacting.SetActive(true);
            interactingSlider.maxValue = useWeapon.interactTime;
            interactingSlider.value = interactTimer;
            interactTimer -= Time.deltaTime;
        }
        else {
            interacting.SetActive(false);
        }

        if (useWeapon.CanPurchase()) {
            purchaseText.gameObject.SetActive(true);
        }
        else { purchaseText.gameObject.SetActive(false); }

    }

}
