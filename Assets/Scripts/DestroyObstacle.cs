using UnityEngine;

public class DestroyObstacle : MonoBehaviour
{
    float cameraY;
    public float destoyOffset;

    // Update is called once per frame
    void Update()
    {
        float objectY = this.transform.position.y;
        cameraY = Camera.main.transform.position.y - destoyOffset;

        if(cameraY > objectY) {
            Destroy(this.gameObject);
        }
    }
}
