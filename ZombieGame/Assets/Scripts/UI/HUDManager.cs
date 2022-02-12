using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {
    private GameObject player;
    private GameObject[] buyZones;
    private UseWeapon useWeapon;
    private Points playerPoints;
    private float buyRange = 3;

    [SerializeField] private TextMeshProUGUI gunText;
    [SerializeField] private TextMeshProUGUI magText;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI pointText;
    [SerializeField] private TextMeshProUGUI transactionText;
    public TextMeshProUGUI purchaseText;

    [SerializeField] private GameObject reloading;
    [HideInInspector] public float reloadTimer;
    private Slider reloadingSlider;

    [SerializeField] private GameObject switching;
    [HideInInspector] public float switchTimer;
    private Slider switchingSlider;


    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        useWeapon = player.GetComponent<UseWeapon>();
        playerPoints = player.GetComponent<Points>();
        buyZones = GameObject.FindGameObjectsWithTag("Buy Zone");
        reloadingSlider = reloading.GetComponentInChildren<Slider>();
        switchingSlider = switching.GetComponentInChildren<Slider>();
    }

    void Update() {
        gunText.text = useWeapon.primaryWeapon.WeaponName;
        magText.text = useWeapon.primaryWeapon.CurrentMag.ToString();
        ammoText.text = useWeapon.primaryWeapon.ReserveAmmo.ToString();
        pointText.text = playerPoints.currentPoints.ToString();
        transactionText.text = playerPoints.lastTransaction;

        foreach (GameObject element in buyZones) {
            if (Vector3.Distance(player.transform.position, element.transform.position) < buyRange) {
                purchaseText.gameObject.SetActive(true);
            }
            else { purchaseText.gameObject.SetActive(false); }
        }

        if (useWeapon.isReloading) {
            reloading.SetActive(true);
            reloadTimer -= Time.deltaTime;
            reloadingSlider.maxValue = useWeapon.primaryWeapon.ReloadSpeed;
            reloadingSlider.value = reloadTimer;
        }
        else {
            reloading.SetActive(false);
            reloadTimer = useWeapon.primaryWeapon.ReloadSpeed;
        }

        if (useWeapon.isSwitching) {
            switching.SetActive(true);
            switchTimer -= Time.deltaTime;
            switchingSlider.maxValue = useWeapon.secondaryWeapon.DrawTime;
            switchingSlider.value = switchTimer;
        }
        else {
            switching.SetActive(false);
            switchTimer = useWeapon.secondaryWeapon.DrawTime;
        }

    }

}
