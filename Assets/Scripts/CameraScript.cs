using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    public Transform target;

    public Transform[] backgrounds;
    //Transform highestEl;
    Transform lowestEl;

    float bgTravelDistance = 12.354f;
    public float camOffset;

    public GameObject parent;
    GameObject[] bgs;
    GameObject newBg;
    GameObject highestEl;

    float highestPos;
    

    // Start is called before the first frame update
    void Start() {
        //highestEl = backgrounds[2];
        //lowestEl = backgrounds[0];

        bgs = GameObject.FindGameObjectsWithTag("Background");

        highestEl = bgs[2];

        for(int i = 0; i <= 2; i++) {
            if(bgs[i].GetComponent<Transform>().position.y > highestEl.GetComponent<Transform>().position.y) {
                highestEl = bgs[i];
                highestPos = bgs[i].GetComponent<Transform>().position.y;
            }
        }
    }

    void Update() {
        // Infinite Background
        if(transform.position.y > highestEl.GetComponent<Transform>().position.y) {
            newBg = Resources.Load("Background 1") as GameObject;

            Instantiate(newBg, new Vector3(0, highestEl.GetComponent<Transform>().position.y + bgTravelDistance, 0), Quaternion.identity);

        }


        /*for (int i = 0; i <= 2; i++) {
            if (backgrounds[i].transform.position.y > highestEl.transform.position.y) {
                highestEl = backgrounds[i];
            }

            if (backgrounds[i].transform.position.y < lowestEl.transform.position.y) {
                lowestEl = backgrounds[i];
            }
        }

        if (transform.position.y > highestEl.transform.position.y - bgTravelDistance) {
            lowestEl.position = new Vector3(backgrounds[0].position.x, highestEl.position.y + bgTravelDistance, backgrounds[0].position.z);
        }*/
    }

    void FixedUpdate() {

        // Player follow
        Vector3 targetPos = new Vector3(0, target.position.y + GetComponent<Camera>().orthographicSize - camOffset, -10);
        transform.position = Vector3.Lerp(transform.position, targetPos, 0.2f);
    }
}
