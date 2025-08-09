using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<int> resMatrix;
    public List<int> inventory;
    public List<Color> keys;

    void Start()
    {
        int[][] temp = GameManager.currentTask.answer;
        for (int i = 0; i < temp.Length; i++)
            for (int j = 0; j < temp[i].Length; j++)
                resMatrix.Add(temp[i][j]);
    }

    public void CheckCorrectness()
    {
        for (int i = 0; i < inventory.Count; i++)
            if (inventory[i] != resMatrix[i])
            {
                if (GameManager.GM.singlePlayer)
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
