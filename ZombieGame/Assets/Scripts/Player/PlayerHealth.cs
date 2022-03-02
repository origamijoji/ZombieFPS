using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int hitsTaken;
    public int maxHits;
    public double regenSpeed;
    public double regenTimer;
    void Start()
    {
        hitsTaken = 0;
    }

    void Update()
    {
        if(hitsTaken > 0) {
            regenTimer += Time.deltaTime;
        }
        if(regenTimer > regenSpeed) {
            hitsTaken = 0;
            regenTimer = 0;
        }
    }

    private void DoesPlayerDie() {
        if(hitsTaken >= maxHits) {
            SceneManager.LoadScene(1);
        }
    }

    public void TakeDamage() {
        hitsTaken++;
        regenTimer = 0;
        DoesPlayerDie();
    }
}
