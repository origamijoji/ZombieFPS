using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;
    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        
    }

    private void PlayerDeath() {

    }

    public void TakeDamage(float dmg) {
        health -= dmg;
    }
}
