using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrentRoundUI : RoundUI
{
    [SerializeField] private TextMeshProUGUI thisText;
    public override void OnUpdate()
    {
        thisText.text = "Round\n " + RoundManager.instance.currentRound.ToString();
    }

}
