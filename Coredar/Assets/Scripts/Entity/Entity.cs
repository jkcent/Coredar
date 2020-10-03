using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    public bool nuetral = true;
    private bool angry = false;
    public float health = 100f;
    public float defence = 0f;
    private bool isDead = false;
    public Material testDead;
    public Rigidbody rb;
    public GameObject player;
    public float moveSpd = 200f;
    public float range = 5f;
    public float attackRange = 1.5f;

    private void Start() {
        if (nuetral)
            angry = false;
        else
            angry = true;
    }

    private void Update() {
        if (angry) {
            MoveTowardsPlayer();
            // Attack
        } else {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    private void LateUpdate() {
        CheckHealth();
    }

    void CheckHealth() {
        if (health <= 0) {
            isDead = true;
            GetComponent<MeshRenderer>().material = testDead;
        } else {
            isDead = false;
        }
    }

    void MoveTowardsPlayer() {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        Vector3 dir = player.transform.position - transform.position;
        dir = dir.normalized;
        dir.y = 0;
        transform.forward = dir;
        //Debug.Log(dist);
        if (dist >= 1.5f && dist <= range) {
            rb.velocity = transform.forward * Time.deltaTime * moveSpd;
        }
    }

    public void TakeDamage(float damage) {
        if (!isDead) {;
            float scale = 128; // Decrase to make steeper curve
            health -= Mathf.CeilToInt(damage / Mathf.Exp(defence / scale));
            Debug.Log(health);
            if (!angry)
                angry = true;
        } 
    }
}
