using UnityEngine;
using TMPro;

public class MagazineAmmoUI : AmmoUI
{
    [SerializeField] private TextMeshProUGUI thisUI;
    public override void OnUpdate()
    {
        thisUI.text = UseWeapon.Instance.primaryWeapon.CurrentMag.ToString();
    }
}
