using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    public bool nuetral = true;
    private bool angry = false;
    public int level = 1;
    public float health = 100f;
    public float defence = 0f;
    private bool dead = false;
    public Material testDead;
    public CharacterController controller;
    GameObject player;
    public Transform attackOrigin;
    public float moveSpd = 200f;
    public float range = 5f;
    public float attackRange = 1.5f;
    public float attackSpeed = 1f;
    public float minAttackDamage = 8f;
    public float maxAttackDamage = 10f;

    private void Start() {
        player = GameObject.Find("Player");

        if (nuetral)
            angry = false;
        else
            angry = true;
    }

    int state = 0;
    float elapsedTime = 0f;
    Vector3 playerLastPosition;
    float boredTimer = 0f;
    Vector3 velocity;
    private void Update() {
        if (angry) {
            //MoveTowardsPlayer();
            switch (state) {
                case 0:
                    #region Wander
                    // Not locked, wandering or some shit idk
                    FindPlayer(1);
                    break;
                    #endregion
                case 1:
                    #region Move Towards Player
                    if (CheckSightLine(player.transform.position)) {
                        float ppJajajaja = Vector3.Distance(player.transform.position, transform.position);
                        if (ppJajajaja <= attackRange) {
                            elapsedTime = 0;
                            state = 3;
                        } else {
                            playerLastPosition = player.transform.position;
                            Move(player.transform.position);
                        }
                    } else {
                        boredTimer = 5f;
                        state = 2;
                    }
                    break;
                    #endregion
                case 2:
                    #region Move Toward Player Last Position
                    float dist = Vector3.Distance(playerLastPosition, transform.position);
                    if (dist <= range && dist > 0.25f && boredTimer > 0) {
                        Move(playerLastPosition);
                        boredTimer -= Time.deltaTime;
                        FindPlayer(1);
                    } else {
                        state = 0;
                    }
                    break;
                    #endregion
                case 3:
                    #region Attack
                    elapsedTime += Time.deltaTime;
                    if (elapsedTime >= attackSpeed) {
                        Attack();
                        state = 1;
                    }
                    break;
                    #endregion
                default:
                    break;
            }
        }
        controller.Move(Physics.gravity);
    }

    private void LateUpdate() {
        CheckHealth();
    }

    void FindPlayer(int _state) {
        if (CheckSightLine(player.transform.position)) {
            playerLastPosition = player.transform.position;
            state = _state;
        }
    }

    void CheckHealth() {
        if (health <= 0) {
            dead = true;
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            renderer.material = testDead;
        } else {
            dead = false;
        }
    }

    void Move(Vector3 target) {
        Vector3 dir = target - transform.position;
        dir = dir.normalized;
        dir.y = 0;
        transform.forward = dir;
        
        Vector3 velocity = transform.forward * moveSpd * Time.deltaTime;
        velocity.y = 0;
        controller.Move(velocity);
    }

    bool CheckSightLine(Vector3 target) {
        Vector3 dir = target - transform.position;

        if (dir.magnitude <= range) {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, dir.normalized, out hit, range)) {
                if (hit.collider.tag == "Player") {
                    return true;
                }
            }
        }

        return false;
    }

    public void TakeDamage(float damage) {
        if (!dead) {
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
                float attackDamage = Mathf.Ceil(Random.Range(minAttackDamage - 0.9f, maxAttackDamage));
                player.TakeDamage(attackDamage);
            }
        }
    }
}
