using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxMoving : MonoBehaviour {

    float length;
    GameObject cam;

    void Start() {
        cam = GameObject.Find("Main Camera");
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update() {
        if(cam.transform.position.x > transform.position.x + length * 1.5f)
            transform.position += new Vector3(length * 3, 0, 0);
        else if(cam.transform.position.x < transform.position.x - length * 1.5f)
            transform.position -= new Vector3(length * 3, 0, 0);
    }
}