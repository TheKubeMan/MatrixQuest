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
        key,
        keyReader
    }
    public ObjectType type;
    
    public void Interaction(GameObject player)
    {
        switch (type)
        {
            case ObjectType.number:
                Debug.Log("Hey, a number!");
                player.GetComponent<PlayerInventory>().inventory.Add(gameObject.GetComponent<Number>().value);
                player.GetComponent<PlayerInventory>().CheckCorrectness();
                //add a check for being a correct number that was picked at the wrong time to stop it from disappearing
                Destroy(gameObject);
                break;
            case ObjectType.button:
                //activate the linked mechanism... 
                //dunno how to seperate it from this script tho
                Debug.Log("You pressed a button... but nothing happened");
                break;
            case ObjectType.key:
                Debug.Log("Hey, a key!");
                player.GetComponent<PlayerInventory>().keys.Add(gameObject.GetComponent<Key>().value);
                Destroy(gameObject);
                break;
            case ObjectType.keyReader:
                Debug.Log("You tried to use a key... but that's not implemented yet");
                //write a script that checks the color value from player's inventory 
                //and substracts the key until the requirement is fulfilled
                break;
        }
    }
}
