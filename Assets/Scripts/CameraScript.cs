using UnityEngine;

public class CameraScript : MonoBehaviour {
    public Transform target;
    public Transform[] backgrounds;
    public Transform tip;

    public Animator animator;
    
    Transform highestEl;
    Transform lowestEl;
    
    public Vector3 camOffset;
    public float smoothSpeed;
    
    const float bgTravelDistance = 11.98f;
    bool editCam = true;
    
    public static float highestBuildingY;
    public static float tipY;

    // Start is called before the first frame update
    void Start() {
        highestEl = backgrounds[2];
        lowestEl = backgrounds[0];
    }

    void Update() {
        if (Player.topReached && editCam) {
            FinishView();
        }

        // Stop infinite background after player wins
        if (GameManager.gameFinish || animator.GetCurrentAnimatorStateInfo(0).IsName("player-idle-simple")) return;

        InfiniteBg();
    }

    void FixedUpdate() {
        // Pause before start animation
        if (!GameManager.initialPlay || GameManager.gameOver) return;
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

    void InfiniteBg() {
        for (int i = 0; i <= 2; i++) {
            if (backgrounds[i].transform.position.y > highestEl.transform.position.y) {
                highestEl = backgrounds[i];

                highestBuildingY = highestEl.position.y;
            }

            if (backgrounds[i].transform.position.y < lowestEl.transform.position.y) {
                lowestEl = backgrounds[i];
            }
        }

        if (transform.position.y > highestEl.transform.position.y - bgTravelDistance) {
            lowestEl.position = new Vector3(backgrounds[0].position.x, highestEl.position.y + bgTravelDistance, backgrounds[0].position.z);
            tip.position = new Vector3(tip.position.x, highestEl.position.y + bgTravelDistance - 4.84f, tip.position.z);
            tipY = tip.position.y;
        }
    }
}
