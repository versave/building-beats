using UnityEngine;

public class DestroyObstacle : MonoBehaviour
{
    float cameraY;
    public float destoyOffset;

    // Start is called before the first frame update
    void Start()
    {
        cameraY = Camera.main.transform.position.y;
    }

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
