using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public Sprite deathState;

    public float xSpeed;
    public float ySpeed;
    public float jumpHeight;
    public float leftFinishPos;
    public float RightFinishPos;

    public static bool topReached = false;
    
    bool jump = false;
    bool flip = true;

    void FixedUpdate()
    {
        bool gameHasFinished = GameManager.gameFinish && !jump && transform.position.y > CameraScript.tipY - 5;
        bool startEndAnimsRunning = AnimationRunning("player-start") || AnimationRunning("player-idle-simple");

        if (gameHasFinished) {
            PlayFinishAnimation();
            return;
        }

        // Disable controls on initial play and game over
        if (GameManager.initialPlay || GameManager.gameOver) {
            return;
        } else if(!animator.GetBool("Start")) {
            animator.SetBool("Start", true);
        }

        // Disable controls at start and finish animations
        if (startEndAnimsRunning) return;
        ControlPlayer();
    }

    void PlayFinishAnimation() {
        animator.SetBool("Jump", false);

        if (transform.position.y <= CameraScript.tipY + 0.27) {
            // Auto move to top
            MovePlayer(0, ySpeed - 2);
        } else {
            // Reach top and rotate player
            if (transform.position.y > CameraScript.tipY && !topReached) {
                transform.Rotate(transform.rotation.x, transform.rotation.y, 90);
                topReached = true;
            }

            // Move to finish position
            if (xSpeed > 0 && transform.position.x > leftFinishPos) {
                MovePlayer(-xSpeed + 7, 0);
            } else if (xSpeed < 0 && transform.position.x < RightFinishPos) {
                MovePlayer(-xSpeed - 7, 0);
            } else {
                animator.SetBool("Finish", true);

                // Readjust player position
                transform.position = new Vector3(transform.position.x, CameraScript.tipY + 0.3f, transform.position.z);
            }
        }
    }

    void ControlPlayer() {
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

    // Collision with obstacle
    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.CompareTag("Obstacle")) {
            GameManager.gameOver = true;
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

    bool AnimationRunning(string animation) {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animation);
    }

    public void PlayerFall() {
        animator.GetComponent<SpriteRenderer>().sprite = deathState;
        animator.GetComponent<Animator>().enabled = false;

        rb.gravityScale = 1;
        rb.constraints = RigidbodyConstraints2D.None;
    }
}
