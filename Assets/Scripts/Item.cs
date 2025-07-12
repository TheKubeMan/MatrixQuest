using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ObjectType
    {
        number,
        button,
        key
    }
    public ObjectType type;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void Interaction()
    {
        if (type == ObjectType.number)
        {
            Debug.Log("Hey, a number!");
            //add to player inventory's matrix
            Destroy(gameObject);
        }
        else if (type == ObjectType.button)
        {
            //activate the linked mechanism
            Debug.Log("You pressed a button... but nothing happened");
        }
        else
        {
            Debug.Log("Hey, a key!");
            //add to player inventory
            Destroy(gameObject);
        }
    }
}
