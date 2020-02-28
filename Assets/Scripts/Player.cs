using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float upwardSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(transform.position.x, transform.position.y + upwardSpeed, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPos, .2f);
    }
}
