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
    [SerializeField] private TextMeshProUGUI flairText;
    [SerializeField] private TextMeshProUGUI magText;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI pointText;
    [SerializeField] private TextMeshProUGUI transactionText;
    [SerializeField] private TextMeshProUGUI purchaseText;
    [SerializeField] private TextMeshProUGUI roundText;

    [SerializeField] private GameObject reloading;
    private Slider reloadingSlider;

    [SerializeField] private GameObject switching;
    private Slider switchingSlider;

    [SerializeField] private GameObject interacting;
    private Slider interactingSlider;

    [SerializeField] private GameObject chambering;
    private Slider chamberingSlider;

    private RoundManager roundmanager;

    private void Awake() {
        //player components
        player = GameObject.FindGameObjectWithTag("Player");
        useWeapon = player.GetComponent<UseWeapon>();
        playerPoints = player.GetComponent<Points>();
        //local components
        reloadingSlider = reloading.GetComponentInChildren<Slider>();
        switchingSlider = switching.GetComponentInChildren<Slider>();
        interactingSlider = interacting.GetComponentInChildren<Slider>();
        chamberingSlider = chambering.GetComponentInChildren<Slider>();
        //global components
        roundmanager = RoundManager.instance;
    }

    void Update() {
        gunText.text = useWeapon.primaryWeapon.WeaponName;
        flairText.text = useWeapon.primaryWeapon.Flair;
        magText.text = useWeapon.primaryWeapon.CurrentMag.ToString();
        ammoText.text = useWeapon.primaryWeapon.ReserveAmmo.ToString();
        pointText.text = playerPoints.currentPoints.ToString();
        transactionText.text = playerPoints.lastTransaction;

        if (useWeapon.reloadTimer > 0) {
            reloading.SetActive(true);
            reloadingSlider.maxValue = useWeapon.primaryWeapon.ReloadSpeed;
            reloadingSlider.value = useWeapon.reloadTimer;
            useWeapon.reloadTimer -= Time.deltaTime;
        }
        else {
            reloading.SetActive(false);
        }

        if (useWeapon.switchTimer > 0) {
            switching.SetActive(true);
            switchingSlider.maxValue = useWeapon.secondaryWeapon.DrawTime;
            switchingSlider.value = useWeapon.switchTimer;
            useWeapon.switchTimer -= Time.deltaTime;
        }
        else {
            switching.SetActive(false);
        }

        if (useWeapon.chamberTimer > 0) {
            chambering.SetActive(true);
            chamberingSlider.maxValue = useWeapon.primaryWeapon.ChamberTime;
            chamberingSlider.value = useWeapon.chamberTimer;
            useWeapon.chamberTimer -= Time.deltaTime;
        }
        else {
            chambering.SetActive(false);
        }

        if (useWeapon.interactTimer > 0 && useWeapon.isInteracting) {
            interacting.SetActive(true);
            interactingSlider.maxValue = useWeapon.interactTime;
            interactingSlider.value = useWeapon.interactTimer;
            useWeapon.interactTimer -= Time.deltaTime;
        }
        else {
            interacting.SetActive(false);
            useWeapon.interactTimer = 0;
        }


        if (useWeapon.CanPurchase()) {
            purchaseText.gameObject.SetActive(true);
        }
        else { purchaseText.gameObject.SetActive(false); }

        roundText.text = "Round \n" + roundmanager.currentRound;
    }

}
