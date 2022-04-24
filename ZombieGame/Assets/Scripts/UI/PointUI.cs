using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PointUI : MonoBehaviour
{
    void Start()
    {
        UpdateUI.AddPointUI(this);
        Invoke("OnUpdate", 0.5f);
    }
    public abstract void OnUpdate();
}
