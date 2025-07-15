using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<int> resMatrix;
    public List<int> inventory;
    public List<Color> keys;

    public void CheckCorrectness()
    {
        for (int i = 0; i < inventory.Count; i++)
            if (inventory[i] != resMatrix[i])
            {
                //this way of referencing might be changed cuz it's long and might not be efficient
                if (GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().singlePlayer)
                {
                    //add a penalty, like substract from the score or something, idk
                }
                else
                {
                    //give the other player the ability to stop this player for 5-10 seconds
                    //to achieve this stopping effect just disable the player controller component
                    //and also reset and enable the countdown on the canvas
                }
                inventory.RemoveAt(i);
            }
            else
            {
                //leave it in the matrix
                //update the ui to have the number shown
                //also, if the session is in multiplayer, then add a background to the number 
                //to match the player's color to indicate who picked the number up
            }
    }
}
