using UnityEngine;

public class CameraScript : MonoBehaviour {
    public Transform target;
    public Animator animator;
    public Vector3 camOffset;
    
    public float smoothSpeed;
    
    bool editCam = true;

    void FixedUpdate() {
        if (Player.topReached && editCam) {
            FinishView();
        }

        // Pause before start animation
        if (GameManager.initialPlay || GameManager.gameOver) return;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("player-idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("player-start")) return;

        FollowPlayer();
    }

    void FinishView() {
        camOffset.x = 2;
        camOffset.y = 2;
        smoothSpeed /= 2;
        editCam = false;
    }

    void FollowPlayer() {
        Vector3 pos = new Vector3(target.position.x + camOffset.x, target.position.y + camOffset.y, camOffset.z) {
            x = target.position.x < 0 ? -Mathf.Abs(camOffset.x) : Mathf.Abs(camOffset.x)
        };

        Vector3 smoothedPos = Vector3.Lerp(transform.position, pos, smoothSpeed * Time.fixedDeltaTime);
        transform.position = smoothedPos;
    }
}
