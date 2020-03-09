using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;

    public float xSpeed;
    public float ySpeed;
    public float jumpHeight;

    bool jump = false;
    bool gameOver = false;

    void Update() {
        if (Input.GetKeyDown("space")) {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        if (jump) {
            MovePlayer(xSpeed, ySpeed + jumpHeight);
        } else {
            MovePlayer(0, ySpeed);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.CompareTag("Obstacle")) {
            gameOver = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        if (collision.collider.CompareTag("Left") || collision.collider.CompareTag("Right")) {
            collision.collider.isTrigger = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(jump) collision.isTrigger = false;

        if (collision.CompareTag("Left") && jump) {
            xSpeed = Mathf.Abs(xSpeed);
            jump = false;
        } else if (collision.CompareTag("Right") && jump) {
            xSpeed = -Mathf.Abs(xSpeed);
            jump = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if(collision.CompareTag("Left") || collision.CompareTag("Right")) {
            collision.isTrigger = true;
        }
    }

    void MovePlayer(float x, float y) {
        rb.MovePosition(rb.position + new Vector2(x, y) * Time.fixedDeltaTime);
    }
}
