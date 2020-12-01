using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
    public Transform head;
    public Transform player;
    public PlayerManager playerManger;
    private float yaw = 0f;
    private float pitch = 0f;
    public float maxDist = 10f;
    public float zoom = 1f;
    public Vector2 pitchMinMax = new Vector2(-40, 85);

    public float rotationSpeed = 10f;
    //bool alreadyMoving = false;

    void Update() {
        //FirstPerson();
        ThirdPerson();
    }

    void FirstPerson() {
        transform.SetPositionAndRotation(head.position, head.rotation);
        //transform.position = head.position;
    }

    void ThirdPerson() {
        #region Orbit Code
        CamZoom();
        if (!Settings.paused && !Settings.inInventory && !Settings.inNPCMenu) {
            yaw += Input.GetAxisRaw("Mouse X") * Settings.sensitivity;
            pitch += -Input.GetAxisRaw("Mouse Y") * Settings.sensitivity;
            pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);
        }

        Vector3 dir = new Vector3(0f, 0f, -maxDist * zoom);
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        transform.position = head.position + rotation * dir;
        transform.LookAt(head.position);
        #endregion
        #region Occlusion Code
        RaycastHit hit;
        if (Physics.Raycast(head.position, (transform.position - head.position).normalized, out hit, maxDist * zoom)) {
            if (hit.collider.gameObject.layer != player.gameObject.layer) {
                transform.position = hit.point;
                transform.position = Vector3.MoveTowards(transform.position, head.position, 0.5f);
            }
        }
        #endregion
        #region Code to be directional while standing still
        /*
        if (playerManger.moving) {
            Quaternion playerRot = Quaternion.Euler(transform.rotation.eulerAngles.y * Vector3.up);
            if (!alreadyMoving) {
                player.rotation = Quaternion.Slerp(player.rotation, playerRot, rotationSpeed * Time.deltaTime);
                head.rotation = player.rotation;
                if (player.rotation == playerRot) {
                    alreadyMoving = true;
                }
            } else {
                player.rotation = playerRot;
                head.rotation = player.rotation;
            }
        } else {
            alreadyMoving = false;
        }
        */
        #endregion
        Quaternion playerRot = Quaternion.Euler(transform.rotation.eulerAngles.y * Vector3.up);
        player.rotation = playerRot;
    }

    void CamZoom() {
        float ampZoom = zoom * 100;

        if (Input.mouseScrollDelta.y > 0) {
            if (!(ampZoom + 4 > 100))
                zoom = (ampZoom + 4) / 100;
            else
                zoom = 1;
        } else if (Input.mouseScrollDelta.y < 0) {
            if (!(ampZoom - 4 < 14))
                zoom = (ampZoom - 4) / 100;
            else
                zoom = 0.14f;
        }
        if (zoom < 0.14f)
            zoom = 0.14f;
    }
}
