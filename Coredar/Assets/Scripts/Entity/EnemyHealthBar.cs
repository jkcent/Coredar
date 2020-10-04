using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour {

    public Text tempBar;
    public Entity entity;
    public Transform playerCam;
    public Transform position;
    Quaternion rotation;

    void Update() {
        Vector3 difference = transform.position - entity.transform.position;
        tempBar.text = $"{Mathf.FloorToInt(entity.health)} hp";
        transform.position = position.position;
        transform.LookAt(transform.position + playerCam.forward);
    }
}
