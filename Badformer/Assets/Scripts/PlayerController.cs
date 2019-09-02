using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public enum Playerstate { Normal, SpeedBoosted, SlowedDown, MessedUp, Jumparound };

    float horizontalInput;
    float moveSpeed = 500f;
    float jumpForce = 30f;
    float groundSensorDepth = 1.5f;
    float sensorShift = .5f;

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

        if(ps == Playerstate.Jumparound && randomJumpTimer < 0) {
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
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position + Vector3.left * sensorShift, Vector2.down, groundSensorDepth, LayerMask.GetMask("Ground"));
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position + Vector3.right * sensorShift, Vector2.down, groundSensorDepth, LayerMask.GetMask("Ground"));
        if(hitLeft.collider != null|| hitRight.collider != null) {
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

    private void OnDrawGizmosSelected() {
        Debug.DrawLine(transform.position + Vector3.left * sensorShift, transform.position + (Vector3.left * sensorShift) + Vector3.down * groundSensorDepth);
        Debug.DrawLine(transform.position + Vector3.right * sensorShift, transform.position + (Vector3.right * sensorShift) + Vector3.down * groundSensorDepth);
    }

    void Jump() {
        if(jump) {
            rb.AddRelativeForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jump = false;
        }
    }

    void MoveHorizontal() {
        float trueSpeed = moveSpeed;
        if (ps == Playerstate.SlowedDown) {
            trueSpeed = moveSpeed / 5;
        } else if(ps == Playerstate.SpeedBoosted) {
            trueSpeed = moveSpeed * 2;
        } else if (ps == Playerstate.MessedUp) {
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
