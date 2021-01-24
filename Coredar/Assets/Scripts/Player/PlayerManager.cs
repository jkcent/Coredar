using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerManager : MonoBehaviour {
    #region variables
    [Header("----- Gameplay -----")]
    public long purse = 0;
    [Header("----- Movement -----")]
    public Transform head;
    public Transform cam;
    public CharacterController controller;
    [SerializeField] private float moveSpd = 5f; // 5 - 10
    [SerializeField] private float turnTime = 0.1f;
    private float turnVelocity;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 1f; // 1
    //[SerializeField] private float pushPower = 3f;
    //private float xRotation = 0f;
    bool jump = false;
    public bool moving = false; 
    [Header("----- NPC -----")]
    public float interactionRange = 10f;
    #endregion

    void Start() {
        #region Movement
        /* Variables */
        transform.rotation = Quaternion.Euler(Vector3.zero);
        head.rotation = Quaternion.Euler(Vector3.zero);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //menu.SetActive(false);
        //Settings.paused = false;
        #endregion
    }

    void Update() {
        //
        CheckStats();
        //
        #region Movement
        if (!Settings.paused) {
            GetExtraInput();
            CheckNPC();
        }
        Move();
        #endregion
    }

    void FixedUpdate() {
        #region Pause FixedUpdate
        if (!Settings.paused) {
            Jump();
        }
        #endregion
    }

    void CheckNPC() {
        RaycastHit hit;
        if (Physics.Raycast(head.position, transform.forward, out hit, interactionRange)) {
            if (Input.GetKeyDown(Settings.accessKey) && hit.collider.tag == "NPC") {
                hit.collider.GetComponent<NPC>().StartInteraction();
            }
        }
    }

    #region Movement
    Vector3 velocity;
    void Move() {
        /* Input */
        if (!Settings.paused) {
            float xMomentum = Input.GetAxisRaw("Horizontal");
            float zMomentum = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(xMomentum, 0, zMomentum).normalized;
            /* Move */
            if (controller.isGrounded && velocity.y <= 0) {
                velocity.y = gravity;
            }
            /*
            Vector3 momentum = transform.right * xMomentum + transform.forward * zMomentum;
            //
            if (momentum != Vector3.zero) { moving = true; } else { moving = false; }
            //
            */
            if (direction.magnitude >= 0.1f) {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
                controller.Move(moveDir.normalized * moveSpd * Time.deltaTime);
            }
        }
        if (!controller.isGrounded) { // Might delete if statement and just leave gravity
            velocity.y += gravity * Time.deltaTime;
        }
        controller.Move(velocity * Time.deltaTime);
    }

    void Jump() {
        /* Jump */
        if (jump) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    void GetExtraInput() {
        if (controller.isGrounded) {
            if (Input.GetKeyDown(Settings.jumpKey)) {
                jump = true;
            }
        } else {
            jump = false;
        }
    }
    /*
    void Look() {
        float mouseX = Input.GetAxisRaw("Mouse X") * Settings.sensitivity;
        float mouseY = -Input.GetAxisRaw("Mouse Y") * Settings.sensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        head.transform.localRotation = Quaternion.Euler(-xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
    */
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
        if (gameObject.tag != "Dead") { // Death Model Object thingy
            float defence = Stats.defence.finalValue;
            float scale = 128; // Decrease to make steeper curve
            Stats.health.finalValue -= Mathf.CeilToInt(damage/Mathf.Exp(defence / scale));
            Debug.Log(Mathf.CeilToInt(damage / Mathf.Exp(defence / scale)));
        }
    }
    void CheckStats() {
        if (Stats.health.finalValue <= 0) {
            gameObject.tag = "Dead";
        }
    }
    /*
    void SetArmoredHealth() {

    }
    */
    #endregion
}