using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    Transform target;
    Vector2 offset = new Vector2(-3, 3);
    void Start() {
        target = GameObject.Find("Player").transform;
    }

    void Update() {
        transform.position = new Vector3(target.position.x + offset.x, target.position.y + offset.y, -10);
        transform.rotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
    }
}
