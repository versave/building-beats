using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;

    public float xSpeed;
    public float ySpeed;
    public float jumpHeight;

    public static bool gameOver = false;

    bool jump = false;
    bool flip = true;

    void FixedUpdate()
    {
        if (!GameManager.gameIsPlaying) {
            return;
        } else if(GameManager.gameIsPlaying && !animator.GetBool("Start")) {
            animator.SetBool("Start", true);
        }

        // Pause before start animation
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("player-start")) return;
        
        if (jump) {
            MovePlayer(xSpeed, ySpeed + jumpHeight);
            animator.SetBool("Jump", true);

            if(flip) {
                transform.Rotate(0, 180, 180);
                flip = false;
            }
        } else {
            MovePlayer(0, ySpeed);
            animator.SetBool("Jump", false);
            flip = true;
        }
        
        if (Input.touchCount > 0 || Input.GetKey(KeyCode.Space)) {
            jump = true;
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
        if (jump) collision.isTrigger = false;

        if (collision.CompareTag("Left") && jump) {
            jump = false;
            xSpeed = Mathf.Abs(xSpeed);
            
        } else if (collision.CompareTag("Right") && jump) {
            jump = false;
            xSpeed = -Mathf.Abs(xSpeed);
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
