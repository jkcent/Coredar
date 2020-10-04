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
    public Transform attackOrigin;
    public float moveSpd = 200f;
    public float range = 5f;
    public float attackRange = 1.5f;
    public float attackSpeed = 1f;
    public float minAttackDamage = 8f;
    public float maxAttackDamage = 10f;

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
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            renderer.material = testDead;
        } else {
            isDead = false;
        }
    }

    float elapsedTime = 0f;
    void MoveTowardsPlayer() {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        Vector3 dir = player.transform.position - transform.position;
        dir = dir.normalized;
        dir.y = 0;
        transform.forward = dir;
        //Debug.Log(dist);
        if (dist >= attackRange && dist <= range) {
            rb.velocity = transform.forward * Time.deltaTime * moveSpd;
        } else if (dist <= attackRange) {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= attackSpeed) {
                Attack();
                elapsedTime = elapsedTime % attackSpeed;
            }
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

    public void Attack() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange)) {
            if (hit.collider.tag == "Player") { // (hit.collider.gameObject.layer == Entity && hit.collider.gameObject != gameObject) for all entities
                PlayerManager player = hit.collider.gameObject.GetComponent<PlayerManager>();
                if (player == null) {
                    return;
                }
                float attackDamage = Mathf.Floor(Random.Range(minAttackDamage, maxAttackDamage + 1));
                player.TakeDamage(attackDamage);
            }
        }
    }
}
