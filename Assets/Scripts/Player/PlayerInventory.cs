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
    int correctCount = 0;
    bool inMatrix = false;
    public int score = 0;

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
        inMatrix = false;
        foreach (int num in resMatrix)
            if (num == number.GetComponent<Number>().value)
                inMatrix = true;

        for (int i = 0; i < inventory.Count; i++)
            if (inventory[i] != resMatrix[i])
            {
                if (GameManager.GM.singlePlayer)
                    if (!inMatrix)
                    {
                        correctCount = 0;
                        Destroy(number);
                    }
                    else
                    {
                        correctCount--;
                        if (correctCount < 0)
                            correctCount = 0;
                    }
                else
                {
                    //give the other player the ability to stop this player for 5-10 seconds
                    //to achieve this stopping effect just disable the player controller component
                    //and add a visual effect to the screen of stopped player (inverse or smth)
                    //and also reset and enable the countdown on the canvas
                }
                inventory.RemoveAt(i);
                return;
            }

        correctCount++;
        //make sure that getting every number correct will  guarantee a good mark
        //but to get the best mark you'd need to also have a good time
        score += 300 + (correctCount * 100);
        Destroy(number);
        //change to updating score ui instead
        Debug.Log(score);
        Debug.Log(correctCount);

        //update the task ui to have the number shown
        //also, if the session is in multiplayer, then add a background to the number 
        //to match the player's color to indicate who picked the number up

    }
}
