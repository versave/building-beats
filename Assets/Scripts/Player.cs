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

    void OnCollisionExit2D(Collision2D collision) {
        collision.collider.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(jump) collision.isTrigger = false;

        if (collision.CompareTag("left") && jump) {
            xSpeed = Mathf.Abs(xSpeed);
            jump = false;
        } else if (collision.CompareTag("right") && jump) {
            xSpeed = -Mathf.Abs(xSpeed);
            jump = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        collision.isTrigger = true;
    }

    void MovePlayer(float x, float y) {
        rb.MovePosition(rb.position + new Vector2(x, y) * Time.fixedDeltaTime);
    }

    /*
    IEnumerator ExecuteAfterTime(float time, Collider2D obj) {
        yield return new WaitForSeconds(time);

        // Code

        //StartCoroutine(ExecuteAfterTime(1, collision));  Call
    }
    */
}
