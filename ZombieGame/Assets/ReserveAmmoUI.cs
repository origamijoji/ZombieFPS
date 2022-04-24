using UnityEngine;
using TMPro;

public class ReserveAmmoUI : AmmoUI
{
    [SerializeField] private TextMeshProUGUI thisUI;
    public override void OnUpdate()
    {
        thisUI.text = UseWeapon.Instance.primaryWeapon.ReserveAmmo.ToString();
    }
}
