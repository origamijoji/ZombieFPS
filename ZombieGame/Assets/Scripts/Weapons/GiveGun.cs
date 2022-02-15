using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GiveGun : MonoBehaviour {

    /// <summary>
    /// This script lets you edit its values in the inspector, and the player will call them when they purchase a weapon
    /// The UI of the script will activate only when the player is within range of it.
    ///
    /// For multiplayer implementation: The physUI will have to either check for all players, or be constantly enabled
    /// </summary>

    [Tooltip("Name of weapon being sold")]
    public string weapon;
    [Tooltip("Cost of currently sold weapon")]
    public int cost;
    [Tooltip("cost of ammo for the currently sold weapon")]
    public int ammoCost;
    [Tooltip("The range the player must be for the UI to enable on the child object")]
    private float UIRange;


    [HideInInspector]
    public Type weaponType;
    private GameObject player;
    private PhysicalUI physUI;

    private void Awake() {
        //player components
        player = GameObject.FindGameObjectWithTag("Player");

        //local components
        physUI = gameObject.GetComponentInChildren<PhysicalUI>();
    }

    void Start() {
        weaponType = Type.GetType(weapon);
    }
    private void Update() {

        if (Vector3.Distance(gameObject.transform.position, player.gameObject.transform.position) < 3) {
            physUI.gameObject.SetActive(true);
        }
        else { physUI.gameObject.SetActive(false); }
    }

}
