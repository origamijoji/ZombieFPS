using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
    public GameObject player;
    public Points points;
    public UseWeapon useWeapon;
    private void Awake() {
        points = player.GetComponent<Points>();
        useWeapon = player.GetComponent<UseWeapon>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { points.AddPoints(100); }
        if (Input.GetKeyDown(KeyCode.F1)) { useWeapon.SpawnWeapon("Pistol"); }
        if (Input.GetKeyDown(KeyCode.F2)) { useWeapon.SpawnWeapon("Carbine"); }
        if (Input.GetKeyDown(KeyCode.F3)) { useWeapon.SpawnWeapon("Sniper"); }
        if (Input.GetKeyDown(KeyCode.F4)) { useWeapon.SpawnWeapon("Revolver"); }
        if (Input.GetKeyDown(KeyCode.F5)) { useWeapon.SpawnWeapon("Shotgun"); }
        if (Input.GetKeyDown(KeyCode.F6)) { useWeapon.SpawnWeapon("Maxim"); }
        if (Input.GetKeyDown(KeyCode.F7)) { useWeapon.SpawnWeapon("Infinity"); }
        if (Input.GetKeyDown(KeyCode.F8)) { }
        if (Input.GetKeyDown(KeyCode.F9)) { }
    }
}
