using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {
    public Transform head;

    void Update() {
        transform.SetPositionAndRotation(head.position, head.rotation);
        //transform.position = head.position;
    }
}
