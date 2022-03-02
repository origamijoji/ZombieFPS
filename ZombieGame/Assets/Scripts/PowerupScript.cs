using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : MonoBehaviour {
    public string powerupType;
    [SerializeField] private double duration;
    public double timeLeft;

    [Header("~ Visual")]
    [SerializeField] private float spinSpeed;
    [SerializeField] private float bobSpeed;
    [SerializeField] private float bounceTime;

    private RoundManager roundManager;
    private Vector3 upPos;
    private Vector3 downPos;
    private bool direction;
    [SerializeField] private float timer;

    private GameObject UI;
    private Vector3 UIPos;
    public bool lockText;

    public delegate void PowerupEvent();
    public static event PowerupEvent MaxAmmo;
    public static event PowerupEvent InstaKill;

    private void Awake() {
        roundManager = RoundManager.instance;
        UI = gameObject.GetComponentInChildren<Canvas>().gameObject;
    }
    private void OnEnable() {
        timeLeft = duration;
        timer = 0;
        direction = false;
        Invoke("UpdatePosition", 0.01f);
    }
    private void OnDisable() {
        lockText = false;
    }
    private void UpdatePosition() {
        upPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.25f, gameObject.transform.position.z);
        downPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.25f, gameObject.transform.position.z);
        UIPos = upPos;
        lockText = true;
        gameObject.transform.position = upPos;

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
        if(lockText) {
            UI.transform.position = UIPos;
        } else { return; }
        timer += Time.deltaTime;

        if (timer < bounceTime) {
            gameObject.transform.position = Vector3.MoveTowards(transform.position, SwitchPos(), Time.deltaTime * bobSpeed);
        }
        else { direction = !direction; timer = 0; }

        gameObject.transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f);
    }

    private Vector3 SwitchPos() {
        switch(direction) {
            case true:
                return upPos;
            case false:
                return downPos;
        }
    }
}
