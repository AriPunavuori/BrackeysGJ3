using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxShifting : MonoBehaviour {
    public float horizontalParallax;
    public float verticalParallax;
    float camPevPosX;
    float camStartPosX;
    GameObject player;

    void Start() {
        player = GameObject.Find("Player");
    }

    void Update() {
        transform.position += new Vector3((player.transform.position.x - camPevPosX) * horizontalParallax, transform.position.y, transform.position.z);
        transform.position = new Vector3(transform.position.x, player.transform.position.y * verticalParallax, transform.position.x);
        camPevPosX = player.transform.position.x;
    }
}
