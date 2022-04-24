using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoundUI : MonoBehaviour
{
    void Start()
    {
        UpdateUI.AddRoundUI(this);
    }
    public abstract void OnUpdate();
}
