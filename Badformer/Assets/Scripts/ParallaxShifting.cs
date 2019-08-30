using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxShifting : MonoBehaviour {
    public float horizontalParallax;
    public float verticalParallax;
    float camPevPosX;
    float camStartPosX;
    GameObject cam;

    void Start() {
        cam = GameObject.Find("Main Camera");
    }

    void Update() {
        transform.position += new Vector3((cam.transform.position.x - camPevPosX) * horizontalParallax, transform.position.y, transform.position.z);
        transform.position = new Vector3(transform.position.x, cam.transform.position.y * verticalParallax, transform.position.x);
        camPevPosX = cam.transform.position.x;
    }
}
