using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieHealth : MonoBehaviour
{
    public float health;
    public Slider healthBar;
    //private RoundManager roundManager;

    private void Awake() {
        healthBar = gameObject.GetComponentInChildren<Slider>();
        //roundManager = GameObject.Find("Round Manager").GetComponent<RoundManager>();
    }

    void Update()
    {
        healthBar.value = health;
    }

    public void TakeDamage(float dmg) {
        health -= dmg;
        ZombieDeath();
    }
    public void TakeDamage(float dmg, float multiplier) {
        health -= dmg * multiplier;
            ZombieDeath();
    }

    private void ZombieDeath() {
        if(health <= 0) {
            gameObject.SetActive(false);

        }
    }

    private void PrepZombieNextSpawn() {
        //stuff here
    }
}
