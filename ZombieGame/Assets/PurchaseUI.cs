using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PurchaseUI : MonoBehaviour
{
    public TextMeshProUGUI stats;
    public void UpdateText(int magCap, int ammo, float damage, float fireRate) {
        stats.text =
            "Magazine Size:" + magCap + "\n" +
            "Ammo Capacity:" + ammo + "\n" +
            "Damage" + damage + "\n" +
            "Fire Rate" + (60 / fireRate);
    }
}
