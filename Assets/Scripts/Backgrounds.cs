using UnityEngine;

public class Backgrounds : MonoBehaviour
{
    public Transform[] backgrounds;
    public Transform leftCollider;
    public Transform rightCollider;
    public Transform tip;
    public GameObject roof;
    public Animator animator;

    Transform camTransform;
    Transform highestEl;
    Transform lowestEl;

    const float bgTravelDistance = 11.98f;

    public static float highestBuildingY;
    public static float tipY;

    // Start is called before the first frame update
    void Start()
    {
        highestEl = backgrounds[2];
        lowestEl = backgrounds[0];
    }

    // Update is called once per frame
    void Update()
    {
        camTransform = Camera.main.transform;

        // Stop infinite background after player wins
        if (GameManager.gameFinish || animator.GetCurrentAnimatorStateInfo(0).IsName("player-dance")) {
            PlaceRoof();
            return;
        }

        InfiniteBg();
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

        if (camTransform.position.y > highestEl.transform.position.y - bgTravelDistance) {
            lowestEl.position = new Vector3(backgrounds[0].position.x, highestEl.position.y + bgTravelDistance, backgrounds[0].position.z);
            tip.position = new Vector3(tip.position.x, highestEl.position.y + bgTravelDistance - 4.84f, tip.position.z);
            tipY = tip.position.y;

            leftCollider.position = new Vector3(leftCollider.position.x, leftCollider.position.y + bgTravelDistance, leftCollider.position.z);
            rightCollider.position = new Vector3(rightCollider.position.x, rightCollider.position.y + bgTravelDistance, rightCollider.position.z);
        }
    }

    void PlaceRoof() {
        roof.transform.position = new Vector3(roof.transform.position.x, tip.position.y - 1.03400f, roof.transform.position.z);
        roof.SetActive(true);
    }
}
