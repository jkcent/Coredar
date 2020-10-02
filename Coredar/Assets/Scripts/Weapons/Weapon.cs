using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour {

    private KeyCode attackKey;
    private Animator animator;
    [Header("Logic")]
    public Transform cam;
    public LayerMask entity;
    [Header("Stats")]
    public float range = 1.5f;
    public float minDmg = 3f;
    public float maxDmg = 5f;

    void Start() {
        attackKey = Settings.attackKey;
        animator = gameObject.GetComponent<Animator>();
    }

    
    void Update() {
        if (!Settings.paused) {
            if (Input.GetKeyDown(attackKey)) {
                CheckAttack();
            }
        }
    }

    void CheckAttack() {
        animator.SetTrigger("Attack");
        /* Hit */
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.TransformDirection(Vector3.forward), out hit, range, entity)) {
            Entity target = hit.collider.gameObject.GetComponent<Entity>();
            if (target == null)
                return;
            Debug.Log($"Hit {hit.collider.name}");
            float dmg = 0f;
            int rand = Mathf.FloorToInt(Random.Range(1f, (maxDmg - minDmg) + 1.99f));
            for (int i = 1; i <= (maxDmg - minDmg) + 1; i++) {
                if (rand == i) {
                    dmg = minDmg + rand - 1f;
                    break;
                }
            }
            //Debug.Log(dmg.ToString());
            target.Damage(dmg);
        }
    }
}
