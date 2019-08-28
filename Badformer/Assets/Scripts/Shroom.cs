using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shroom : MonoBehaviour {

    public PlayerController.Playerstate ps;
    PlayerController pc;
    GameManager gm;

    void Start() {
        pc = FindObjectOfType<PlayerController>();
        gm = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        pc.EatShroom(ps);
        gm.SetUIText(ps.ToString());
        Destroy(gameObject);
    }
}
