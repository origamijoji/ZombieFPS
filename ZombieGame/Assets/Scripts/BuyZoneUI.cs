using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuyZoneUI : MonoBehaviour
{
    private GiveGun giveGun;
    public TextMeshProUGUI statsText;
    private void Awake() {
        giveGun = gameObject.GetComponentInParent<GiveGun>();
    }
    private void Start() {
        statsText.text = MagazineText() + AmmoText() + DamageText() + RPMText() + CritText() + WeightText();
    }
    public string RPMText () {
        return "RPM: " + giveGun.RPM + "\n";
    }
    public string DamageText() {
        return "Damage " + giveGun.damage + "\n";
    }
    public string CritText() {
        return "Crit Value: " + giveGun.critValue + "x" + "\n";
    }
    public string WeightText() {
        return "Weight: " + giveGun.weight + "\n";
    }
    public string MagazineText() {
        return "Magazine Size: " + giveGun.magSize + "\n";
    }
    public string AmmoText() {
        return "Ammo Capacity: " + giveGun.reserveAmmo + "\n";
    }
}
