using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;
    public Vector3 destination;
    public float speed;
    public float time;
    public bool moving = true;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        destination = pointB;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            if (transform.position == destination)
            {
                moving = false;
                Invoke("ChangeDestination", time);
            }
        }
    }

    void ChangeDestination()
    {
        if (destination == pointA)
            destination = pointB;
        else if (destination == pointB)
            destination = pointA;
        moving = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            other.transform.parent = this.transform;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            other.transform.parent = null;
    }
}
