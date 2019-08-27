using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    float horizontalInput;
    public float moveSpeed;
    Rigidbody2D rb2d;

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update() {
        horizontalInput = Input.GetAxisRaw("Horizontal");

    }

    private void FixedUpdate() {
        rb2d.MovePosition(new Vector2(transform.position.x + (horizontalInput * moveSpeed * Time.fixedDeltaTime),transform.position.y + 0));
    }
}
