using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalUI : MonoBehaviour {

    public GameObject player;
    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {
        transform.LookAt(player.transform);
    }
}
