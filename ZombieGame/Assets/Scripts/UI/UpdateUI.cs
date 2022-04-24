using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UpdateUI
{
    private static List<AmmoUI> ammoUIList = new List<AmmoUI>();
    private static List<PointUI> pointUIList = new List<PointUI>();
    private static List<RoundUI> roundUIList = new List<RoundUI>();

    public static void AddAmmoUI(AmmoUI newUI) { ammoUIList.Add(newUI); }
    public static void Ammo()
    {
        foreach (AmmoUI ui in ammoUIList)
        {
            ui.OnUpdate();
        }
    }
    public static void AddRoundUI(RoundUI newUI) { roundUIList.Add(newUI); }
    public static void Round()
    {
        foreach (RoundUI ui in roundUIList)
        {
            ui.OnUpdate();
        }
    }
    public static void AddPointUI(PointUI newUI) { pointUIList.Add(newUI); }
    public static void Point()
    {
        foreach(PointUI ui in pointUIList)
        {
            ui.OnUpdate();
        }
    }
}
