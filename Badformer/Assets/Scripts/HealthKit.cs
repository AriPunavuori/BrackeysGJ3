using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKit : MonoBehaviour {

    public AudioSource audioSource;
    public AudioClip healthAudio;

    GameManager gm;
    public bool bad;
    int healthToAdd = 10;

    void Start() {
        gm = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
            audioSource.PlayOneShot(healthAudio);
            if(bad) {
                gm.SetHealth(-healthToAdd);
            } else {
                gm.SetHealth(healthToAdd);
            }
            Destroy(gameObject);
        }
    }
}
