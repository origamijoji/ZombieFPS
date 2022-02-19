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
        RoundManager.NewRound += UpdateStats;
    }

    void Update() {
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
        if (health <= 0) {
            gameObject.SetActive(false);
            roundManager.ZombieDeath();
            PrepZombieNextSpawn();
        }
    }

    private void PrepZombieNextSpawn() {
        health = roundManager.currentHealth;
        healthBar.maxValue = health;
        roundManager.QueueZombie(gameObject);
    }

    private void UpdateStats() {
        health = roundManager.currentHealth;
        healthBar.maxValue = health;
    }
}
