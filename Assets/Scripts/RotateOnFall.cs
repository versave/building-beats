using UnityEngine;

public class RotateOnFall : MonoBehaviour
{
    Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotation = new Vector3(0f, 0f, Random.Range(0f, rigidBody.velocity.y - 0.5f));
        transform.Rotate(rotation);
    }
}
