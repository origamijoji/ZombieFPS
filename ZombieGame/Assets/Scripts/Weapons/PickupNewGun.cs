using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupNewGun : MonoBehaviour
{
    public int cost;
    public Weapon newGun;
    public GameObject player;
    public UseWeapon playerUseWeapon;

    void Start()
    {
        newGun = new Rifle();
        player = GameObject.FindGameObjectWithTag("Player");
        playerUseWeapon = player.GetComponent<UseWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        //playerUseWeapon.PickupGun(Rifle);
    }
}
