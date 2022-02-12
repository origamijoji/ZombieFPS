using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
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
    private Slider reloadingSlider;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        useWeapon = player.GetComponent<UseWeapon>();
        playerPoints = player.GetComponent<Points>();
        buyZones = GameObject.FindGameObjectsWithTag("Buy Zone");
        reloadingSlider = reloading.GetComponentInChildren<Slider>();
    }

    void Update() {
        gunText.text = useWeapon.currentWeapon.WeaponName;
        magText.text = useWeapon.currentWeapon.CurrentMag.ToString();
        ammoText.text = useWeapon.currentWeapon.ReserveAmmo.ToString();
        pointText.text = playerPoints.currentPoints.ToString();
        transactionText.text = playerPoints.lastTransaction;

        foreach(GameObject element in buyZones) {
            if (Vector3.Distance(player.transform.position, element.transform.position) < buyRange) {
                purchaseText.gameObject.SetActive(true);
            }
            else { purchaseText.gameObject.SetActive(false); }
        }

    }

}
