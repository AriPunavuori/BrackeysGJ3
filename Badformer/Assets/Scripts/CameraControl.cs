using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    Transform target;

    void Start() {
        target = GameObject.Find("Player").transform;
    }


    void Update() {
        transform.position = new Vector3(target.position.x, target.position.y, -10);
    }
}
