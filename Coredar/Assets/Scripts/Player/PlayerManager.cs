using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerManager : MonoBehaviour {

    public Transform head;
    public Transform isGroundedPos;
    public CharacterController controller;
    public GameObject menu;
    [SerializeField] private float moveSpd = 10;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float pushPower = 3f;
    private float xRotation = 0f;
    public bool isGrounded = false;
    bool jump = false;
    private bool dead = false;

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
        //Debug.Log(Stats.health.finalValue);
        /*
        if (Input.GetKeyDown(KeyCode.G)) {
            TakeDamage(10f);
        }*/
        #region Pause Update
        if (!Settings.paused && !dead) {
            GetExtraInput();
            Look();
            Move();
            if (Cursor.visible && !Settings.paused) {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        } else {
            if (!Cursor.visible) {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
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
        #region Pause FixedUpdate
        if (!Settings.paused && !dead) {
            Jump();
        }
        #endregion
    }
    #region Movement
    Vector3 velocity;
    void Move() {
        /* Input */
        float xMomentum = Input.GetAxisRaw("Horizontal") * moveSpd * Time.deltaTime;
        float zMomentum = Input.GetAxisRaw("Vertical") * moveSpd * Time.deltaTime;
        //float xMomentum = Input.GetAxisRaw("Horizontal") * moveSpd * Time.deltaTime;
        //float zMomentum = Input.GetAxisRaw("Vertical") * moveSpd * Time.deltaTime;
        /* Move */
        if (isGrounded && velocity.y <= 0) {
            velocity.y = -2f;
        }

        Vector3 momentum = transform.right * xMomentum + transform.forward * zMomentum;
        #region
        /*
        if (xMomentum != 0 && zMomentum != 0) {
            Mathf.Sqrt(momentum.x);
            Mathf.Sqrt(momentum.z);
        }
        if (!isGrounded)
            rb.velocity = new Vector3(momentum.x, rb.velocity.y, momentum.z);
        else if (isGrounded)
            rb.velocity = new Vector3(momentum.x, 0f, momentum.z);
        */
        #endregion
        controller.Move(momentum);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void CheckGrounded() {
        isGrounded = false;
        Collider[] collisions = Physics.OverlapSphere(isGroundedPos.position, 0.4f);
        for (int i = 0; i < collisions.Length; i++) {
            if (collisions[i].tag != "Player") {
                isGrounded = true;
                break;
            }
        }
        //rb.useGravity = !isGrounded;
    }

    void Jump() {
        /* Jump */
        if (jump) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
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
        float mouseX = Input.GetAxisRaw("Mouse X") * Settings.sensitivity;
        float mouseY = -Input.GetAxisRaw("Mouse Y") * Settings.sensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        head.transform.localRotation = Quaternion.Euler(-xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
    #endregion
    #region Push Rigidbodies
    /*
    void OnControllerColliderHit(ControllerColliderHit hit) {
        Rigidbody body = hit.collider.attachedRigidbody;
        //
        if (body == null || body.isKinematic)
            return;
        if (hit.moveDirection.y < -0.3)
            return;
        //
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0f, hit.moveDirection.z);
        body.velocity = pushDir * pushPower;
    }
    */
    #endregion
    #region Stats
    public void TakeDamage(float damage) {
        if (!dead) {
            float defence = Stats.defence.finalValue;
            float scale = 128; // Decrease to make steeper curve
            Stats.health.finalValue -= Mathf.CeilToInt(damage/Mathf.Exp(defence / scale));
            Debug.Log(Mathf.CeilToInt(damage / Mathf.Exp(defence / scale)));
        }
    }
    void CheckStats() {
        if (Stats.health.finalValue <= 0) {
            dead = true;
        }
    }
    /*
    void SetArmoredHealth() {

    }
    */
    #endregion
}