using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    public Transform target;
    public Transform[] backgrounds;
    
    Transform highestEl;
    Transform lowestEl;
    
    public Vector3 camOffset;
    public float smoothSpeed;
    
    const float bgTravelDistance = 12.31f;

    // Start is called before the first frame update
    void Start() {
        highestEl = backgrounds[2];
        lowestEl = backgrounds[0];
    }

    void Update() {
        InfiniteBg();
    }

    void FixedUpdate() {
        FollowPlayer();
    }

    void FollowPlayer() {
        Vector3 pos = new Vector3(target.position.x + camOffset.x, target.position.y + camOffset.y, camOffset.z);
        pos.x = target.position.x < 0 ? -Mathf.Abs(camOffset.x) : Mathf.Abs(camOffset.x);

        Vector3 smoothedPos = Vector3.Lerp(transform.position, pos, smoothSpeed * Time.fixedDeltaTime);
        transform.position = smoothedPos;
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
