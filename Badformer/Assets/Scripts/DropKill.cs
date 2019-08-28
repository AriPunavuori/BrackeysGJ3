using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropKill : MonoBehaviour {

    GameManager gm;
    PlayerController pc;

    private void Start() {
        gm = FindObjectOfType<GameManager>();
        pc = FindObjectOfType<PlayerController>();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
            gm.SetLives(-1);
            pc.TeleportToCheckpoint(gm.Checkpoint());
        }
    }
}
