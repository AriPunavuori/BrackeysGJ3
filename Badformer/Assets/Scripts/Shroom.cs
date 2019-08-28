using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shroom : MonoBehaviour {

    public PlayerController.Playerstate ps;
    PlayerController pc;
    
    void Start() {
        pc = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        pc.EatShroom(ps);
        Destroy(gameObject);
    }
}
