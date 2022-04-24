using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    public int currentPoints;
    public string lastTransaction;

    public float transactionTimer;
    private float transactionTime = 1.5f;

    private static Points _instance;
    public static Points Instance
    {
        get => _instance;
    }
    private void Awake()
    {
        _instance = this;
    }
    public void AddPoints(int value) {
        transactionTimer = transactionTime;
        currentPoints += value;
        lastTransaction = "+" + value;
    }

    public void AddPoints(int value, float mult) {
        transactionTimer = transactionTime;
        currentPoints += Mathf.RoundToInt(value * mult);
        lastTransaction = "+" + value * mult;
    }

    public void RemovePoints(int value) {
        transactionTimer = transactionTime;
        currentPoints -= value;
        lastTransaction = "-" + value;
    }

    private void Update() {
        if (transactionTimer > 0) {
            transactionTimer -= Time.deltaTime;
        }
        else {
            lastTransaction = string.Empty;
        }
    }

    public bool CanAfford(int cost) {
        if(cost <= currentPoints) {
            return true;
        }
        else {
            return false;
        }
    }

}
