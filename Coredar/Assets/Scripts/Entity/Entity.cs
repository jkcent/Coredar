using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    public bool nuetral;
    public float health = 100f;
    public Material testDead;

    private void LateUpdate() {
        if (health <= 0) {
            tag = "Dead";
            GetComponent<MeshRenderer>().material = testDead;
        }
    }

    public void Damage(float dmg) {
        if (tag != "Dead")
            health -= dmg;
        else
            return;
    }
}
