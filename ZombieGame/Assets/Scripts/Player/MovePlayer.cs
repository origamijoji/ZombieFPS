using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public Rigidbody rb;
    public Transform orientation;
    public float moveSpeed;
    public float walkSpeed;
    public float acceleration;
    public float runSpeed = 8;
    public float gravity;
    public float groundDrag = 7;
    public float airDrag;
    private Vector3 moveDir;
    private float xInput;
    private float zInput;
    public bool isGrounded = true;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        GetAxis();
        moveDir = xInput * orientation.right + zInput * orientation.forward;

        ApplyMovement();
        DragControl();
        SprintControl();
    }

    private void GetAxis() {
        xInput = Input.GetAxisRaw("Horizontal");
        zInput = Input.GetAxisRaw("Vertical");
    }
    private void ApplyMovement() {
        rb.AddForce(moveDir.normalized * moveSpeed, ForceMode.Acceleration);
    }

    private void DragControl() {
        rb.drag = groundDrag;
    }
    private void SprintControl() {
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded) {
            moveSpeed = Mathf.Lerp(moveSpeed, runSpeed, acceleration * Time.deltaTime);
        }
        else { moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime); }
    }
}
