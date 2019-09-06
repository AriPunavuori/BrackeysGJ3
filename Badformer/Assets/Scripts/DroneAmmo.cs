using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAmmo : MonoBehaviour {
    public float ammoSpeed = 10f;

    void Start() {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.Find("Player");
        Vector2 target = player.transform.position;
        Quaternion rotation = Quaternion.LookRotation(target-(Vector2)transform.position, Vector2.up);
        transform.rotation = rotation;
        rb.isKinematic = false;
        Vector2 targetVector = target - (Vector2)transform.position;
        rb.AddForce(targetVector.normalized * ammoSpeed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {
            GameManager gm = FindObjectOfType<GameManager>();
            gm.SetHealth(-10);
            Destroy(gameObject);
        } else {
            print(collision.gameObject);
            Destroy(gameObject);
        }
    }

}
