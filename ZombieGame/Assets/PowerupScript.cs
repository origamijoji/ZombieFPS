using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : MonoBehaviour
{
    public string powerupType;
    public double duration;
    public double timeLeft;
    public float spinSpeed;

    private RoundManager roundManager;

    public delegate void PowerupEvent();
    public static event PowerupEvent MaxAmmo;
    public static event PowerupEvent InstaKill;

    private void Awake() {
        roundManager = RoundManager.instance;
    }
    private void OnEnable() {
        timeLeft = duration;
    }
    private void Update() {
        PowerupVisual();
        if(timeLeft > 0) {
            timeLeft -= Time.deltaTime;
        }
        else {
            DisablePowerup();
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")) {
            switch(powerupType) {
                case "MaxAmmo":
                    MaxAmmo?.Invoke();
                    break;
                case "InstaKill":
                    InstaKill?.Invoke();
                    break;
            }
            DisablePowerup();
        }
    }
    private void OnValidate() {
        timeLeft = duration;
    }

    private void DisablePowerup() {
        gameObject.SetActive(false);
        roundManager.DisablePowerup(powerupType);
    }

    private void PowerupVisual() {
        gameObject.transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f);
    }
}
