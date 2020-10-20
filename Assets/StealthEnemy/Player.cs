using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthEnemy {

public class Player : MonoBehaviour {

    public float speed;

    private Rigidbody2D rb2d;
    private Vector2 userInput;

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (Input.GetButton("Vertical") || Input.GetButton("Horizontal")) 
        {
            userInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
    }

    void FixedUpdate() {
        rb2d.MovePosition(rb2d.position + userInput.normalized * speed * Time.deltaTime);
    } 
}

}
