using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public int score = 0, total = 0;
    public GameObject VictoryScreen, LevelStuff;
    public GameObject taskScore, taskScoreFin, timeScore, totalScore, gradeUI;

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
        score += 300 + (correctCount * 100);
        Destroy(number);
        taskScore.GetComponent<TextMeshProUGUI>().text = score.ToString();
        //change to updating score ui instead
        Debug.Log(correctCount);

        if (inventory.Count == resMatrix.Count)
        {
            EndGame();
        }

        //update the task ui to have the number shown
        //also, if the session is in multiplayer, then add a background to the number 
        //to match the player's color to indicate who picked the number up

    }
    void EndGame()
    {
        LevelStuff.SetActive(false);
        gameObject.GetComponent<PlayerController>().enabled = false;
        VictoryScreen.SetActive(true);

        Camera.main.GetComponent<Timer>().Finish();
        int time = Camera.main.GetComponent<Timer>().finalTime;
        int timeS, grade;
        int size = GameManager.currentSize;

        int t1 = 0, t2 = 0, t3 = 0;
        int s1 = 0, s2 = 0, s3 = 0;
        switch (size)
        {
            case 0:
                t1 = 50 * 100;
                t2 = 90 * 100;
                t3 = 130 * 100;
                s1 = 2800;
                s2 = 2500;
                s3 = 1900;
                break;
            case 1:
                t1 = 90 * 100;
                t2 = 135 * 100;
                t3 = 180 * 100;
                break;
            case 2:
                t1 = 135 * 100;
                t2 = 180 * 100;
                t3 = 250 * 100;
                break;
            case 3:
                t1 = 160 * 100;
                t2 = 210 * 100;
                t3 = 255 * 100;
                break;
            case 4:
                t1 = 200 * 100;
                t2 = 280 * 100;
                t3 = 330 * 100;
                break;
        }

        if (time <= t1)
            timeS = 900;
        else if (time > t1 && time <= t2)
            timeS = 600;
        else if (time > t2 && time < t3)
            timeS = 300;
        else
            timeS = 0;

        total = score + timeS;

        if (total >= s1)
            grade = 5;
        else if (total < s1 && total >= s2)
            grade = 4;
        else if (total < s2 && total >= s3)
            grade = 3;
        else
            grade = 2;

        int[] fin = new int[2];
        fin[0] = total;
        fin[1] = time;
        switch (size)
        {
            case 0:
                GameManager.Record2x2.Add(fin);
                break;
            case 1:
                GameManager.Record2x3.Add(fin);
                break;
            case 2:
                GameManager.Record3x3.Add(fin);
                break;
            case 3:
                GameManager.Record3x4.Add(fin);
                break;
            case 4:
                GameManager.Record4x4.Add(fin);
                break;
        }

        SaveSystem.SaveResults(size);

        taskScoreFin.GetComponent<TextMeshProUGUI>().text = score.ToString();
        timeScore.GetComponent<TextMeshProUGUI>().text = timeS.ToString();
        totalScore.GetComponent<TextMeshProUGUI>().text = total.ToString();
        gradeUI.GetComponent<TextMeshProUGUI>().text = grade.ToString();
    }
}
