using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieHealth : MonoBehaviour
{
    public float health;
    public Slider healthBar;

    private void Awake() {
        healthBar = gameObject.GetComponentInChildren<Slider>();
    }

    void Update()
    {
        if(health <= 0) {
            gameObject.SetActive(false);
        }

        healthBar.value = health;
    }

    public void TakeDamage(float dmg) {
        health -= dmg;
    }
    public void TakeDamage(float dmg, float multiplier) {
        health -= dmg * multiplier;
    }
}
