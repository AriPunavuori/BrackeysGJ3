using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrone : MonoBehaviour {
    public GameObject ammoPrefab;

    float FiringDist = 15f;
    int ammoBurstAmount = 5;
    int ammoAmount = 0;
    float shotInterval = .1f;
    float firingInterval = 1.5f;
    float shotTimer;
    float firingTimer;

    public bool inSight;
    public bool startFiring;

    GameObject player;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {
        firingTimer -= Time.deltaTime;
        shotTimer -= Time.deltaTime;

        if(inSight) {
            if(firingTimer < 0) {
                startFiring = true;
            }
        }
        if(startFiring) {
            Fire();
        }
    }

    private void FixedUpdate() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position);
        if(hit.collider.gameObject == player && hit.distance < FiringDist) {
            inSight = true;
        } else {
            inSight = false;
        }
    }

    void Fire() {
        if(ammoAmount < ammoBurstAmount) {
            if(shotTimer < 0) {
                Instantiate(ammoPrefab, transform.position, Quaternion.identity);
                shotTimer = shotInterval;
                ammoAmount++;
            }

        } else {
            firingTimer = firingInterval;
            ammoAmount = 0;
            startFiring = false;
        }

    }
}
