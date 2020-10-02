using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [Header("Components and Objects")]
    public Transform head;
    public Rigidbody rb;
    [Header("Values")]
    public float sensitivity = 100;
    [SerializeField] private float moveSpd = 0;
    [SerializeField] private float jumpForce = 0;
    static public bool isGrounded = false;
    bool jump = false;
    [Header("Key Codes")]
    public KeyCode jumpKey = KeyCode.Space;

    void Start() {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update() {
        GetExtraInput();
    }

    void FixedUpdate() {
        Look();
        Move();
    }

    float ClampAngle(float angle, float from, float to) {
        if (angle < 0f)
            angle = 360 + angle;
        if (angle > 180f)
            return Mathf.Max(angle, 360 + from);
        return Mathf.Min(angle, to);
    }

    void Move() {
        /* Input */
        float xMomentum = Input.GetAxisRaw("Horizontal");
        float zMomentum = Input.GetAxisRaw("Vertical");
        /* Move */
        transform.position += (transform.right * xMomentum + transform.forward * zMomentum) * moveSpd * Time.deltaTime;
        /* Jump */
        if (jump) {
            rb.AddForce(new Vector3(0f, jumpForce));
        }
    }

    void GetExtraInput() {
        if (isGrounded) {
            if (Input.GetKeyDown(jumpKey)) {
                jump = true;
            }
        } else {
            jump = false;
        }
    }

    void Look() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        /* Input */
        float multiplier = 4f;
        float xRot = Input.GetAxisRaw("Mouse X") * sensitivity * multiplier;
        float yRot = -Input.GetAxisRaw("Mouse Y") * sensitivity * multiplier;
        //Debug.Log($"({xRot}, {yRot})");
        /* Rotate X */
        transform.Rotate(new Vector3(0, xRot * Time.deltaTime), Space.Self);
        /* Rotate Y */
        Vector3 rot = head.rotation.eulerAngles + new Vector3(yRot * Time.deltaTime, 0f);
        rot.x = ClampAngle(rot.x, -90f, 90f);
        
        head.eulerAngles = rot;
    }
}
