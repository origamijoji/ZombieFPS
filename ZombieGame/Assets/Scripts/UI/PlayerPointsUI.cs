using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerPointsUI : PointUI
{
    [SerializeField] private TextMeshProUGUI thisText;
    public override void OnUpdate()
    {
        thisText.text = "$" + Points.Instance.currentPoints.ToString();
    }
}
