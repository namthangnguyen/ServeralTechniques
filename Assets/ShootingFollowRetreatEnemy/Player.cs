using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Rigidbody2D rb2d;

    public float speed;
    private Vector2 moveVelocity;

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update() {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveVelocity = moveInput.normalized * speed;
    }

    void FixedUpdate() {
        rb2d.MovePosition(rb2d.position + moveVelocity * Time.deltaTime);
    }
}
