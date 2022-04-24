using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AmmoUI : MonoBehaviour
{
    void Start()
    {
        UpdateUI.AddAmmoUI(this);
        Invoke("OnUpdate", 0.5f);
    }
    public abstract void OnUpdate();
}
