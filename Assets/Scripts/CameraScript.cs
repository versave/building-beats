using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    public Transform target;

    public RectTransform[] backgrounds;
    RectTransform highestEl;
    RectTransform lowestEl;

    float bgTravelDistance = 8.829999f;

    // Start is called before the first frame update
    void Start() {
        highestEl = backgrounds[2];
        lowestEl = backgrounds[0];
    }

    void Update() {
        // Infinite Background
        for (int i = 0; i <= 2; i++) {
            if (backgrounds[i].transform.position.y > highestEl.transform.position.y) {
                highestEl = backgrounds[i];
            }

            if (backgrounds[i].transform.position.y < lowestEl.transform.position.y) {
                lowestEl = backgrounds[i];
            }
        }

        if (transform.position.y > highestEl.transform.position.y - 1) {
            lowestEl.position = new Vector3(backgrounds[0].position.x, highestEl.position.y + bgTravelDistance, backgrounds[0].position.z);
        }
    }

    void FixedUpdate() {

        // Player follow
        Vector3 targetPos = new Vector3(0, target.position.y, -10);
        transform.position = Vector3.Lerp(transform.position, targetPos, 0.2f);
    }
}
