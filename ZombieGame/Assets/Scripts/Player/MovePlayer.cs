using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour {

    private float moveSpeed;
    public float walkSpeed;
    public float acceleration;
    public float runSpeed;
    public float gravity;
    public float groundCheckRadius;
    public float groundDrag = 7;
    public float airDrag;


    public Transform feet;
    public Rigidbody rb;
    public Transform orientation;
    public LayerMask whatIsGround;

    private Vector3 moveDir;
    private float xInput;
    private float zInput;
    private bool isGrounded;

    [SerializeField] private float zoomMultiplier;
    [SerializeField] private float lockMultiplier;

    void Start() {
        LockMovement(false);
    }

    void Update() {
        GetAxis();
        moveDir = xInput * orientation.right + zInput * orientation.forward;
        CheckGround();
        DragControl();
        SprintControl();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }
    private void GetAxis() {
        xInput = Input.GetAxisRaw("Horizontal");
        zInput = Input.GetAxisRaw("Vertical");
    }
    private void ApplyMovement() {
        rb.AddForce(moveDir.normalized * moveSpeed * lockMultiplier * zoomMultiplier, ForceMode.Acceleration);
    }

    private void DragControl() {
        if (isGrounded) { rb.drag = groundDrag; }
        else { rb.drag = airDrag; }
    }

    private void CheckGround() {
        if (Physics.CheckSphere(feet.position, groundCheckRadius, whatIsGround)) {
            isGrounded = true;
        }
        else { isGrounded = false; }
    }
    private void SprintControl() {
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded) {
            moveSpeed = Mathf.Lerp(moveSpeed, runSpeed, acceleration * Time.deltaTime);
        }
        else { moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime); }
    }

    public void LockMovement(bool locked) {
        if (locked) {
            lockMultiplier = 0;
        }
        else {
            lockMultiplier = 1;
        }
    }
    public void ZoomedIn(float value) {
        zoomMultiplier = value;
    }
    public void UnZoom() {
        zoomMultiplier = 1;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(feet.position, groundCheckRadius);
    }
}
