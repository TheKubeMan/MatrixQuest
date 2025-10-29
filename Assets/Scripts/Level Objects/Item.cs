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
                player.GetComponent<PlayerInventory>().CheckCorrectness(gameObject);
                break;
            case ObjectType.button:
                //activate the linked mechanism using a unity event, check KeyReader.cs to see how it's done
                Debug.Log("You pressed a button... but nothing happened");
                break;
            case ObjectType.key:
                if (player.GetComponent<PlayerInventory>().keys.Count < 5)
                {
                    player.GetComponent<PlayerInventory>().keys.Add(gameObject.GetComponent<Key>().value);
                    player.GetComponent<PlayerInventory>().DrawInventory();
                    Destroy(gameObject);
                }
                else
                    //this must be changed into a message popup later
                    Debug.Log("Inventory is full");
                break;
            case ObjectType.keyReader:
                gameObject.GetComponent<KeyReader>().Interaction(player);
                break;
        }
    }
}
