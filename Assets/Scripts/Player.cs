using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;

    public float xSpeed;
    public float ySpeed;

    bool jump = false;

    void Update() {
        if (Input.GetKeyDown("space")) {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        if (jump) {
            MovePlayer(xSpeed, ySpeed + 10);
        } else {
            MovePlayer(0, ySpeed);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log(collision.collider.tag);

        if(collision.collider.tag == "left" && jump) {
            xSpeed = Mathf.Abs(xSpeed);
            jump = false;
        } else if(collision.collider.tag == "right" && jump) {
            xSpeed = -Mathf.Abs(xSpeed);
            jump = false;
        }
    }

    void MovePlayer(float x, float y) {
        rb.MovePosition(rb.position + new Vector2(x, y) * Time.fixedDeltaTime);
    }
}
