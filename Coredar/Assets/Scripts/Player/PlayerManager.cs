using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    [Header("Components and Objects")]
    public Transform head;
    public Rigidbody rb;
    public GameObject menu;
    [Header("Values")]
    private float sensitivity;
    [SerializeField] private float moveSpd = 0;
    [SerializeField] private float jumpForce = 0;
    public static bool isGrounded = false;
    bool jump = false;
    [Header("Key Codes")]
    private KeyCode jumpKey;
    private KeyCode pauseKey;

    void Start() {
        /* Variables */
        rb = gameObject.GetComponent<Rigidbody>();
        transform.rotation = Quaternion.Euler(Vector3.zero);
        head.rotation = Quaternion.Euler(Vector3.zero);
        menu.SetActive(false);
        Settings.paused = false;
        /* Settings */
        SetValues();
    }

    void Update() {
        SetValues();
        //
        if (!Settings.paused) {
            GetExtraInput();
        } else {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        //
        if (Input.GetKeyDown(pauseKey)) {
            if (Settings.paused) {
                Settings.paused = false;
                menu.SetActive(false);
            } else if (!Settings.paused) {
                Settings.paused = true;
                menu.SetActive(true);
            }
        }
    }

    void FixedUpdate() {
        if (!Settings.paused) {
            Move();
            Jump();
        }
    }

    void LateUpdate() {
        if (!Settings.paused) {
            Look();
        }
    }
    #region movement
    void SetValues() {
        sensitivity = Settings.sensitivity;
        jumpKey = Settings.jumpKey;
        pauseKey = Settings.pauseKey;
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
        float xMomentum = Input.GetAxisRaw("Horizontal") * moveSpd * Time.deltaTime;
        float zMomentum = Input.GetAxisRaw("Vertical") * moveSpd * Time.deltaTime;
        /* Move */
        Vector3 momentum = transform.right * xMomentum + transform.forward * zMomentum;
        if (!isGrounded)
            rb.velocity = new Vector3(momentum.x, rb.velocity.y, momentum.z);
        else if (isGrounded)
            rb.velocity = new Vector3(momentum.x, 0f, momentum.z);
    }

    void Jump() {
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
    #endregion
}

public static class Stats {
    public static int strenght = 1;

}