using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    private GameObject player;
    private UseWeapon useWeapon;
    private Points playerPoints;
    [SerializeField] private TextMeshProUGUI gunText;
    [SerializeField] private TextMeshProUGUI magText;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI pointText;
    [SerializeField] private TextMeshProUGUI transactionText;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        useWeapon = player.GetComponent<UseWeapon>();
        playerPoints = player.GetComponent<Points>();
    }

    void Update()
    {
        gunText.text = useWeapon.currentWeapon.WeaponName;
        magText.text = useWeapon.currentWeapon.CurrentMag.ToString();
        ammoText.text = useWeapon.currentWeapon.ReserveAmmo.ToString();
        pointText.text = playerPoints.currentPoints.ToString();
        transactionText.text = playerPoints.lastTransaction;
 
    }
}
