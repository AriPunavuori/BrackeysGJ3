using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IUP : MonoBehaviour {
    GameManager gm;
    public bool bad;
    int livesToAdd = 1;

    void Start() {
        gm = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
            if(bad) {
                gm.SetLives(-livesToAdd);
            } else {
                gm.SetLives(livesToAdd);
            }
            Destroy(gameObject);
        }
    }
}