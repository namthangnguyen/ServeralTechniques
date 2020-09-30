using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed;

    private Rigidbody2D rb2d;
    private Vector2 userInput;
    private Vector2 inputDirecton = new Vector2(1, 0);

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (Input.GetButton("Vertical") || Input.GetButton("Horizontal")) 
        {
            userInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            inputDirecton.Set(userInput.x, userInput.y);
            Debug.Log(inputDirecton);
            float rotationZ = Mathf.Atan2(inputDirecton.y, inputDirecton.x) * Mathf.Rad2Deg;
            // Debug.Log(rotationZ);
            transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
        }
    }

    void FixedUpdate() {
        rb2d.MovePosition(rb2d.position + userInput.normalized * speed * Time.deltaTime);
    }
}
