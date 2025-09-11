using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public List<int> resMatrix;
    public List<int> inventory;
    public List<Color> keys;
    public Image[] keysUI;
    public Sprite Green, Red, Blue, Yellow, Orange, Purple, White, Cyan, Transp;
    Dictionary<Color32, Sprite> keyValues;

    void Start()
    {
        int[][] temp = GameManager.currentTask.answer;
        for (int i = 0; i < temp.Length; i++)
            for (int j = 0; j < temp[i].Length; j++)
                resMatrix.Add(temp[i][j]);

        keyValues = new Dictionary<Color32, Sprite>
        {
            {new Color32 (178, 31, 31, 255), Red},
            {new Color32 (15, 153, 24, 255), Green},
            {new Color32 (52, 83, 221, 255), Blue},
            {new Color32 (222, 200, 13, 255), Yellow},
            {new Color32 (255, 255, 255, 255), White},
            {new Color32 (196, 108, 13, 255), Orange},
            {new Color32 (161, 10, 198, 255), Purple},
            {new Color32 (26, 186, 207, 255), Cyan}
        };
    }

    public void DrawInventory()
    {
        //clearing all slots first
        foreach (Image key in keysUI)
            key.sprite = Transp;

        //updating them with values 
        for (int i = 0; i < 5; i++)
        {
            if (i >= keys.Count)
                break;

            Sprite value;
            keyValues.TryGetValue(keys[i], out value);
            keysUI[i].sprite = value;
        }
    }

    public void CheckCorrectness(GameObject number)
    {
        //add a check to see if the number was in resMatrix, but picked up too early
        //if it was, add a different penalty (dunno, maybe the same) and don't destroy the object
        //if it wasn't then destroy the object and give a stronger penalty
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
