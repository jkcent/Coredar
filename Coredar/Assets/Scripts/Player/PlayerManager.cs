using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public Transform head;
    public Transform isGroundedPos;
    public Rigidbody rb;
    public GameObject menu;
    private float moveSpd = 200;
    private float jumpForce = 300;
    private float xRotation = 0f;
    public bool isGrounded = false;
    bool jump = false;

    void Start() {
        /* Variables */
        transform.rotation = Quaternion.Euler(Vector3.zero);
        head.rotation = Quaternion.Euler(Vector3.zero);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //menu.SetActive(false);
        //Settings.paused = false;
    }

    void Update() {
        CheckStats();
        Debug.Log(Stats.health.finalValue);
        #region Pause
        if (!Settings.paused) {
            GetExtraInput();
            Look();
        } else {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        //
        if (Input.GetKeyDown(Settings.pauseKey)) {
            if (Settings.paused) {
                Settings.paused = false;
                menu.SetActive(false);
            } else if (!Settings.paused) {
                Settings.paused = true;
                menu.SetActive(true);
            }
        }
        #endregion
    }

    void FixedUpdate() {
        if (!Settings.paused) {
            Move();
            Jump();
        }
    }
    #region Movement

    float ClampAngle(float angle, float from, float to) {
        // accepts e.g. -80, 80
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
        if (xMomentum != 0 && zMomentum != 0) {
            Mathf.Sqrt(momentum.x);
            Mathf.Sqrt(momentum.z);
        }
        if (!isGrounded)
            rb.velocity = new Vector3(momentum.x, rb.velocity.y, momentum.z);
        else if (isGrounded)
            rb.velocity = new Vector3(momentum.x, 0f, momentum.z);
    }

    void CheckGrounded() {
        isGrounded = false;
        Collider[] collisions = Physics.OverlapSphere(isGroundedPos.position, 0.4f);
        for (int i = 0; i < collisions.Length; i++) {
            if (collisions[i].tag != "Player")
                isGrounded = true;
        }
        rb.useGravity = !isGrounded;
    }

    void Jump() {
        /* Jump */
        if (jump) {
            rb.AddForce(new Vector3(0f, jumpForce));
        }
        CheckGrounded();
    }

    void GetExtraInput() {
        if (isGrounded) {
            if (Input.GetKeyDown(Settings.jumpKey)) {
                jump = true;
            }
        } else {
            jump = false;
        }
    }

    void Look() {
        /*
        float mouseX = Input.GetAxisRaw("Mouse X") * Settings.sensitivity * Time.deltaTime;
        float mouseY = -Input.GetAxisRaw("Mouse Y") * Settings.sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        head.transform.localRotation = Quaternion.Euler(-xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
        */
        float mouseHorizontalRotation = Input.GetAxis("Mouse X") * Settings.sensitivity * Time.deltaTime;
        transform.Rotate(0, mouseHorizontalRotation, 0);

        xRotation -= Input.GetAxis("Mouse Y") * Settings.sensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        head.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
    #endregion
    #region Stats
    void TakeDamage(float damage) {
        float defence = Stats.defence.finalValue;
        float scale = 128; // Decrase to make steeper curve
        Stats.health.finalValue -= Mathf.CeilToInt(damage/Mathf.Exp(defence / scale));
    }
    void CheckStats() {
        if (Stats.health.finalValue <= 0) {
            tag = "Dead";
        }
    }
    /*
    void SetArmoredHealth() {

    }
    */
    #endregion
}