using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour {

    public AudioSource audioSource;
    public AudioClip creditAudio;

    GameManager gm;
    public bool bad;
    int scoreToAdd = 50;

    void Start() {
        gm = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        audioSource.PlayOneShot(creditAudio);
        if(bad) {
            gm.SetScore(-scoreToAdd);
        } else {
            gm.SetScore(scoreToAdd);
        }
        Destroy(gameObject);
    }
}
