using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolCar : MonoBehaviour {
    Rigidbody2D rb;
    public Vector2 patrolPoint;
    public Vector2 patrolAreaSizeX;
    public Vector2 patrolAreaSizeY;
    GameObject drone;
    Vector2 startPos;
    public float speed;
    float waitTimer;
    float waitTime = 2f;
    bool droneLaunched;

    void Start() {
        drone = transform.Find("EnemyDrone").gameObject;
        drone.gameObject.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        RandomizeNextPoint();
    }

    void Update() {
        
    }

    private void FixedUpdate() {
        var movementDelta = (patrolPoint - (Vector2)transform.position) * Time.deltaTime * speed;
        print("MovementDelta" + movementDelta);
        print("Position:" + ((Vector2)transform.position));
        rb.MovePosition((Vector2)transform.position + movementDelta);
        if(Vector2.Distance(transform.position, patrolPoint) < 0.1f) {
            rb.MovePosition(patrolPoint);
            RandomizeNextPoint();
        }
    }

    void RandomizeNextPoint() {
        patrolPoint.x = startPos.x + Random.Range(patrolAreaSizeX.x, patrolAreaSizeX.y);
        if(patrolPoint.x < transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
        patrolPoint.y = startPos.y + Random.Range(patrolAreaSizeY.x, patrolAreaSizeY.y);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {
            if(!droneLaunched) {
                drone.gameObject.SetActive(true);
                drone.transform.SetParent(null);
                droneLaunched = true;
            }
        }
    }

    public void DroneLost() {
        droneLaunched = false;
        drone.transform.SetParent(this.transform);
        drone.gameObject.SetActive(false);
        
    }
}
