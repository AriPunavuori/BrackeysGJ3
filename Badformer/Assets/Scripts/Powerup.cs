using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

    public PlayerController.Playerstate ps;
    PlayerController pc;
    GameManager gm;

    void Start() {
        pc = FindObjectOfType<PlayerController>();
        gm = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
            Destroy(gameObject);
            pc.EatShroom(ps);
            gm.SetUIText(ps.ToString());
            Destroy(gameObject);
        }
    }
}
