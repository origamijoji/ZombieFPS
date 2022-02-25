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
        DoesZombieDie();
    }

    private void DoesZombieDie() {
        if (health <= 0) {
            gameObject.SetActive(false);
            roundManager.ZombieDeath();
            roundManager.SpawnPowerup(gameObject.transform.position, gameObject.transform.rotation);
            PrepZombieNextSpawn();

        }
    }

    private void PrepZombieNextSpawn() {
        UpdateStats();
        roundManager.QueueZombie(gameObject);
    }

    private void UpdateStats() {
        health = roundManager.currentHealth;
        healthBar.maxValue = health;
    }
}
