using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleControls : MonoBehaviour
{
    float cameraY;
    bool pointGiven = false;
    public float destoyOffset;

    // Update is called once per frame
    void Update()
    {
        float objectY = this.transform.position.y;
        cameraY = Camera.main.transform.position.y - destoyOffset;

        if(Camera.main.transform.position.y - 3 > objectY && !pointGiven) {
            ++GameManager.score;
            pointGiven = true;
        }

        if (cameraY > objectY) {
            Destroy(this.gameObject);
        }
    }
}
