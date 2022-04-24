using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {

    #region Sensitivity
    [Header("~ Sensitivity <3")]
    [Tooltip("Mouse X sensitivity")]
    public float sensX;
    [Tooltip("Mouse Y sensitivity")]
    public float sensY;
    public float defaultFOV = 60f;

    public float currentRecoil;
    public float resetRate;

    private float mouseX;
    private float mouseY;

    [SerializeField] private float lockValue;
    [SerializeField] private float zoomValue;
    #endregion
    #region References
    [Header("~ References <3")]
    [Tooltip("Player Camera (Component not GameObject")]
    public Camera playerCamera;
    [Tooltip("Player orientation object, child of player")]
    public Transform orientation;


    private float multiplier = 0.01f;
    private float xRotation;
    private float yRotation;
    #endregion
    private void Awake() {
    }

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        LockMouseInput(false);
    }

    private void Update() {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sensX * multiplier * lockValue * zoomValue;
        xRotation -= mouseY * sensY * multiplier * lockValue * zoomValue;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        currentRecoil = Mathf.Lerp(currentRecoil, 0, Time.deltaTime * 2);

        playerCamera.transform.rotation = Quaternion.Euler(xRotation - currentRecoil, yRotation, 0);
        orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    public void LockMouseInput(bool locked) {
        if (locked) {
            lockValue = 0;
        }
        else {
            lockValue = 1;
        }
    }

    public void ZoomWeapon(float value) {
        playerCamera.fieldOfView = value;
        zoomValue = 0.3f;
    }

    public void UnZoom() {
        playerCamera.fieldOfView = defaultFOV;
        zoomValue = 1;
    }

    public void Recoil(float amount) {
        currentRecoil += amount;
    }
}

