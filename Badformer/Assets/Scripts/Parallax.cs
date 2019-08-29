using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

    public float horizontalParallax;
    public float verticalParallax;
    float length;
    float camPevPosX;
    float camStartPosX;
    GameObject cam;

    void Start() {
        cam = GameObject.Find("Main Camera");
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update() {
        transform.position += new Vector3((cam.transform.position.x - camPevPosX) * horizontalParallax, transform.position.y, transform.position.z);
        transform.position = new Vector3(transform.position.x, cam.transform.position.y * verticalParallax, transform.position.x);
        camPevPosX = cam.transform.position.x;
        if(cam.transform.position.x > transform.position.x + length * 1.5f)
            transform.position += new Vector3(length * 3, 0, 0);
        else if(cam.transform.position.x < transform.position.x - length * 1.5f)
            transform.position -= new Vector3(length * 3, 0, 0);
    }
}