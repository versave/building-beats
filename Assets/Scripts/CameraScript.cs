using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    public Transform target;
    public Transform[] backgrounds;
    Transform highestEl;
    Transform lowestEl;

    const float bgTravelDistance = 12.31f;
    public float camOffset;

    public static float camSpeed = 0.5f;

    // Start is called before the first frame update
    void Start() {
        highestEl = backgrounds[2];
        lowestEl = backgrounds[0];
    }

    void Update() {
        InfiniteBg();
    }

    void FixedUpdate() {
        //FollowPlayer();
    }

    void FollowPlayer() {
        Vector3 targetPos = new Vector3(0, target.position.y + GetComponent<Camera>().orthographicSize - camOffset, -10);
        transform.position = Vector3.Lerp(transform.position, targetPos, camSpeed);
    }

    void InfiniteBg() {
        for (int i = 0; i <= 2; i++) {
            if (backgrounds[i].transform.position.y > highestEl.transform.position.y) {
                highestEl = backgrounds[i];
            }

            if (backgrounds[i].transform.position.y < lowestEl.transform.position.y) {
                lowestEl = backgrounds[i];
            }
        }

        if (transform.position.y > highestEl.transform.position.y - bgTravelDistance) {
            lowestEl.position = new Vector3(backgrounds[0].position.x, highestEl.position.y + bgTravelDistance, backgrounds[0].position.z);
        }
    }
}
