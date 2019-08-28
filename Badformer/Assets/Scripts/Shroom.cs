using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shroom : MonoBehaviour {

    PlayerController pc;
    public bool bad;
   
    void Start() {
        pc = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(bad) {
            pc.StartSpeedBoost(-2);
        } else {
            pc.StartSpeedBoost(2);
        }
        Destroy(gameObject);
    }
}
