using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
    public Transform head;
    public Transform player;
    public PlayerManager playerManger;
    public LayerMask entityMask;
    private float yaw = 0f;
    private float pitch = 0f;
    public float maxDist = 10f;
    public float zoom = 1f;
    public Vector2 pitchMinMax = new Vector2(-40, 85);

    public float rotationSpeed = 10f;
    //bool alreadyMoving = false;

    void Update() {
        //FirstPerson();
        if (player == null) {
            //ded cam
        } else
            ThirdPerson();
    }

    void FirstPerson() {
        transform.SetPositionAndRotation(head.position, head.rotation);
        //transform.position = head.position;
    }

    void ThirdPerson() {
        if (!Settings.paused) {
            CamZoom();

            yaw += Input.GetAxisRaw("Mouse X") * Settings.sensitivity;
            pitch += -Input.GetAxisRaw("Mouse Y") * Settings.sensitivity;
            pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);
        }

        Vector3 dir = new Vector3(0f, 0f, -maxDist * zoom);
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        transform.position = head.position + rotation * dir;
        transform.LookAt(head.position);

        RaycastHit hit;
        if (Physics.Linecast(head.position, transform.position, out hit, ~entityMask)) {
            transform.position = Vector3.MoveTowards(hit.point, head.position, 2f);
        }
    }

    void CamZoom() {
        float ampZoom = zoom * 100;
        int scrollSize = Settings.zoomSpeed;

        if (Input.mouseScrollDelta.y > 0) {
            if (!(ampZoom + scrollSize > 100))
                zoom = (ampZoom + scrollSize) / 100;
            else
                zoom = 1;
        } else if (Input.mouseScrollDelta.y < 0) {
            if (!(ampZoom - scrollSize < 1))
                zoom = (ampZoom - scrollSize) / 100;
            else
                zoom = 0.01f;
        }
        if (zoom < 0.01f)
            zoom = 0.01f;

        zoom = Mathf.Round(zoom * 100) / 100;
    }
}
