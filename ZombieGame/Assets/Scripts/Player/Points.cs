using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    public int currentPoints;
    public string lastTransaction;

    public float transactionTimer;
    private float transactionTime = 1.5f;
    public void AddPoints(int value) {
        transactionTimer = transactionTime;
        currentPoints += value;
        lastTransaction = "+" + value;
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


}
