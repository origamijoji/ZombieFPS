using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public CharacterController characterController;
    public Transform orientation;
    public float moveSpeed;
    public float gravity;

    private Vector3 moveDir;
    private float xInput;
    private float zInput;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        GetAxis();
        moveDir = xInput * orientation.right + zInput * orientation.forward;

        characterController.Move(moveDir.normalized * moveSpeed);
        ApplyGravity();
    }

    private void GetAxis() {
        xInput = Input.GetAxisRaw("Horizontal");
        zInput = Input.GetAxisRaw("Vertical");
    }

    private void ApplyGravity() {
        characterController.Move(Vector3.down * gravity);
    }
}
