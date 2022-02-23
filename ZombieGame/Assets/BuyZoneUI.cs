using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyZoneUI : MonoBehaviour
{
    public string RPMText (float rpm) {
        return "RPM: " + rpm;
    }
    public string DamageText(float dmg) {
        return "Damage " + dmg;
    }
    public string CritText(float crit) {
        return "Crit Value: " + crit + "x";
    }
    public void WeightText() {

    }
    public void MagazineText() {

    }
    public void AmmoText() {

    }
}
