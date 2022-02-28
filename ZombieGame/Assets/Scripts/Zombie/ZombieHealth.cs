using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieHealth : MonoBehaviour {
    public float health;
    public Slider healthBar;
    public RoundManager roundManager;

    private void Awake() {
        healthBar = gameObject.GetComponentInChildren<Slider>();
        roundManager = RoundManager.instance;
    }

    void Update() {
        healthBar.value = health;
    }

    public void TakeDamage(float dmg, bool instaKilled) {
        health -= dmg;
        if(instaKilled) {
            health = 0;
        }

        DoesZombieDie();
    }

    private void DoesZombieDie() {
        if (health <= 0) {
            gameObject.SetActive(false);
            roundManager.ZombieDeath();
            roundManager.SpawnPowerup(gameObject.transform.position, gameObject.transform.rotation);
            roundManager.QueueZombie(gameObject); ;

        }
    }
    private void OnEnable() {
        UpdateStats();
    }

    private void UpdateStats() {
        health = roundManager.currentHealth;
        healthBar.maxValue = health;
    }
}
