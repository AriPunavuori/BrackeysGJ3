using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public enum Playerstate { Normal, Speedboost, Slowdown, MessUp, Jumper };

    float horizontalInput;
    float moveSpeed = 500f;
    float jumpForce = 30f;
    float accelerationFactor = 5;
    float groundSensorDepth = 1.5f;

    Animator animator;
    GameManager gm;
    Playerstate ps;
    Rigidbody2D rb;
    
    bool facingRight = true;
    bool jump;
    bool onGround;
    bool jumpButtonReleased = true;

    float randomJumpTimer;

    int speedBoost = 1;
    float powerupTime = 5f;
    float powerupTimer;

    void Start() {
        animator = GetComponent<Animator>();
        gm = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

        if(Input.GetKeyUp(KeyCode.Space)) {
            jumpButtonReleased = true;
        }
        if(Input.GetKeyDown(KeyCode.Space) && jumpButtonReleased && onGround) {
            jump = true;
            jumpButtonReleased = false;
        }

        if(ps == Playerstate.Jumper && randomJumpTimer < 0) {
            randomJumpTimer = Random.Range(.3f, 1f);
            jump = true;
        }

        if(powerupTimer < 0) {
            ps = Playerstate.Normal;
        }
        powerupTimer -= Time.deltaTime;
        randomJumpTimer -= Time.deltaTime;
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
        float trueSpeed = moveSpeed;
        if (ps == Playerstate.Slowdown) {
            trueSpeed = moveSpeed / 5;
        } else if(ps == Playerstate.Speedboost) {
            trueSpeed = moveSpeed * 2;
        } else if (ps == Playerstate.MessUp) {
            trueSpeed = -trueSpeed;
        }
        rb.velocity = new Vector2(horizontalInput * trueSpeed * Time.fixedDeltaTime, rb.velocity.y);
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

    public void EatShroom(Playerstate state) {
        ps = state;
        powerupTimer = powerupTime;
    }

    public void TeleportToCheckpoint() {

        rb.velocity = new Vector2(0, 0);
        rb.isKinematic = true;
        var cp = gm.Checkpoint();
        transform.position = cp;
        rb.isKinematic = false;
    }
}
