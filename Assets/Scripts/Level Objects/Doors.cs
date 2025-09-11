using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public float speed;
    Vector3 destination;
    Quaternion rotDest;
    // Start is called before the first frame update
    void Start()
    {
        destination = transform.position;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotDest, speed * Time.deltaTime);
    }
    public void Move(string xyz)
    {
        //since unity events don't support Vector3, the coords must be written as a string
        //the format should always be "x y z" 
        xyz = xyz.Trim();
        string[] move = xyz.Split(' ');
        Vector3 movement = new Vector3(float.Parse(move[0]), float.Parse(move[1]), float.Parse(move[2]));
        destination = transform.position + movement;
    }

    public void Rotate(string xyz)
    {
        //same as Move(), Vector3 isn't supported by unity events
        //the format is also "x y z"
        xyz = xyz.Trim();
        string[] rot = xyz.Split(' ');
        rotDest = Quaternion.Euler(float.Parse(rot[0]), float.Parse(rot[1]), float.Parse(rot[2]));
    }
}
