using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    float horizontalInput;
    float moveSpeed = 1250;
    float jumpForce = 35f;
    float accelerationFactor = 10;
    float groundSensorDepth = 1.5f;
    Rigidbody2D rb;
    
    bool facingRight = true;
    bool jump;
    public bool onGround;
    bool jumpButtonReleased = true;

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
        rb.velocity = new Vector2(horizontalInput * moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
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
}
