using UnityEngine;
using TMPro;
public class FlairAmmoUI : AmmoUI
{
    [SerializeField] private TextMeshProUGUI thisUI;
    public override void OnUpdate()
    {
        thisUI.text = UseWeapon.Instance.primaryWeapon.Flair.ToString();
    }
}
