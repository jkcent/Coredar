using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerManager : MonoBehaviour {
    #region variables
    [Header("----- Movement -----")]
    public Transform head;
    public CharacterController controller;
    public GameObject menu;
    [SerializeField] private float moveSpd = 5; // 5 - 10
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 1f; // 1
    //[SerializeField] private float pushPower = 3f;
    //private float xRotation = 0f;
    bool jump = false;
    public bool moving = false; 
    [Header("----- NPC -----")]
    public LayerMask NPCLayer;
    public float interactionRange = 10f;
    #endregion
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
        #region Pause/Inventory Update
        if (!Settings.paused && gameObject.tag != "Dead" && !Settings.inInventory && !Settings.inNPCMenu) {
            GetExtraInput();
            //Look();
            //Move();
            if (Cursor.visible && !Settings.paused && !Settings.inInventory) {
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
        if (Input.GetKeyDown(Settings.pauseKey) || Input.GetKeyDown(Settings.inventoryKey)) {
            // Open Menus
            if (!Settings.inInventory && !Settings.inNPCMenu && Input.GetKeyDown(Settings.pauseKey)) {
                if (Settings.paused) {
                    Settings.paused = false;
                    menu.SetActive(false);
                } else if (!Settings.paused) {
                    Settings.paused = true;
                    menu.SetActive(true);
                }
            }
            if (!Settings.paused && !Settings.inNPCMenu && Input.GetKeyDown(Settings.inventoryKey)) {
                if (Settings.inInventory) {
                    Settings.inInventory = false;
                    gameObject.GetComponent<PlayerInventory>().playerInventoryList.SetActive(false);
                } else if (!Settings.inInventory) {
                    Settings.inInventory = true;
                    gameObject.GetComponent<PlayerInventory>().playerInventoryList.SetActive(true);
                }
            }
        }
        Move();
        // General Pause Functions
        if (!Settings.paused && !Settings.inInventory && !Settings.inNPCMenu) {
            CheckNPC();
        }
        #endregion 
    }

    void FixedUpdate() {
        #region Pause FixedUpdate
        if (!Settings.paused && !Settings.inInventory && !Settings.inNPCMenu && gameObject.tag != "Dead") {
            Jump();
        }
        #endregion
    }

    void CheckNPC() {
        RaycastHit hit;
        if (Physics.Raycast(head.position, transform.forward, out hit, interactionRange, NPCLayer)) {
            if (Input.GetKeyDown(Settings.accessKey)) {
                hit.collider.GetComponent<NPC>().StartInteraction();
            }
        }
    }

    #region Movement
    Vector3 velocity;
    void Move() {
        /* Input */
        if (!Settings.paused && !Settings.inInventory && !Settings.inNPCMenu && gameObject.tag != "Dead") {
            float xMomentum = Input.GetAxisRaw("Horizontal") * moveSpd * Time.deltaTime;
            float zMomentum = Input.GetAxisRaw("Vertical") * moveSpd * Time.deltaTime;
            //float xMomentum = Input.GetAxisRaw("Horizontal") * moveSpd * Time.deltaTime;
            //float zMomentum = Input.GetAxisRaw("Vertical") * moveSpd * Time.deltaTime;
            /* Move */
            if (controller.isGrounded && velocity.y <= 0) {
                velocity.y = -2f;
            }

            Vector3 momentum = transform.right * xMomentum + transform.forward * zMomentum;
            //
            if (momentum != Vector3.zero) { moving = true; } else { moving = false; }
            //
            controller.Move(momentum);
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
        if (gameObject.tag != "Dead") {
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