using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    float horizontalInput;
    float moveSpeed = 1000;
    float jumpForce = 30f;
    float accelerationFactor = 5;
    float groundSensorDepth = 1.5f;
    Rigidbody2D rb;
    
    bool facingRight = true;
    bool jump;
    public bool onGround;
    bool jumpButtonReleased = true;

    int speedBoost = 1;
    float boostTime = 2f;
    float boostTimer;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if(Input.GetKeyUp(KeyCode.Space)) {
            jumpButtonReleased = true;
        }
        if(Input.GetKeyDown(KeyCode.Space) && jumpButtonReleased && onGround) {
            jump = true;
            jumpButtonReleased = false;
        }
        if(boostTimer < 0) {
            speedBoost = 1;
        }
        boostTimer -= Time.deltaTime;
    }

    private void FixedUpdate() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundSensorDepth, LayerMask.GetMask("Ground"));
        if(hit.collider != null) {
            onGround = true;
        } else {
            onGround = false;
        }
        MoveHorizontal();
        if(onGround) {
            if(jump)
                Jump();
        }     
    }

    void Jump() {
        if(jump) {
            rb.AddRelativeForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jump = false;
        }
    }

    void MoveHorizontal() {
        rb.velocity = new Vector2(horizontalInput * moveSpeed * speedBoost * Time.fixedDeltaTime, rb.velocity.y);
        Flip();
    }

    void Flip() {
        if(horizontalInput > 0) {
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
        } else if(horizontalInput < 0) {
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
        }
    }

    public void StartSpeedBoost(int b) {
        speedBoost = b;
        boostTimer = boostTime;
    }
}
