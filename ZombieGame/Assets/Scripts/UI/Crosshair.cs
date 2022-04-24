using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private Image crosshair;
    private static Image s_crosshair;

    public static void Enable()
    {
        s_crosshair.gameObject.SetActive(true);
    }

    public static void Disable()
    {
        s_crosshair.gameObject.SetActive(false);
    }

    void Start()
    {
        s_crosshair = crosshair;
    }
}
