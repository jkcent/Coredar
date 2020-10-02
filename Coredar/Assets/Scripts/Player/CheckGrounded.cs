using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGrounded : MonoBehaviour {

    public Rigidbody playerRb;

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Ground") {
            PlayerMovement.isGrounded = false;
            playerRb.useGravity = true;
            //Debug.Log("Not Grounded");
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Ground") {
            PlayerMovement.isGrounded = true;
            playerRb.useGravity = false;
            //Debug.Log("Grounded");
        }
    }
}
