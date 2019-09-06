using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAmmo : MonoBehaviour {

    public float ammoSpeed = 10f;
    public GameObject fxRed;
    public GameObject fxYellow;

    void Start() {
        fxRed = transform.Find("ParticleFXRed").gameObject;
        fxYellow = transform.Find("ParticleFXYellow").gameObject;

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
            fxRed.SetActive(true);
            fxRed.transform.parent = null;
            Destroy(gameObject);
        } else {
            fxYellow.SetActive(true);
            fxYellow.transform.parent = null;
            Destroy(gameObject);
        }

    }

}
