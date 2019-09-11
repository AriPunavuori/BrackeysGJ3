using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyDrone : MonoBehaviour {
    public GameObject ammoPrefab;
    public GameObject dummyTarget;

    float FiringDist = 15f;
    int ammoBurstAmount = 5;
    int ammoAmount = 0;
    float shotInterval = .1f;
    float firingInterval = 1.5f;
    float shotTimer;
    float firingTimer;
    float getLostTimer;
    float getLostTime = 5f;
    AIDestinationSetter ds;
    EnemyPatrolCar pc;

    public bool inSight;
    public bool startFiring;
    
    GameObject player;

    private void Awake() {
        pc = transform.parent.GetComponent<EnemyPatrolCar>();
    }

    private void Start() {

        getLostTimer = getLostTime;
        player = GameObject.FindGameObjectWithTag("Player");
        ds = GetComponent<AIDestinationSetter>();
        ds.target = player.transform;
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
            getLostTimer = getLostTime;
            ds.target = player.transform;
        } else {
            inSight = false;
        }
        if(!inSight) {
            getLostTimer -= Time.fixedDeltaTime;
        }
        if (getLostTimer < 0) {
            ds.target = pc.transform;
            if(Vector2.Distance(transform.position, pc.transform.position) < 1.6f) {
                transform.position = pc.transform.position;
                getLostTimer = getLostTime;
                pc.DroneLost();
            }
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

    public void SetPatrolCar(EnemyPatrolCar e) {
        pc = e;
    }
}
