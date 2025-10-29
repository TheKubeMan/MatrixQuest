using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomizer : MonoBehaviour
{
    void Start()
    {
        //get the matrix
        List<int> resMatrix = new List<int>();
        int[][] temp = GameManager.currentTask.answer;
        for (int i = 0; i < temp.Length; i++)
            for (int j = 0; j < temp[i].Length; j++)
                resMatrix.Add(temp[i][j]);
        //add junk fetched from the json

        //shuffle the matrix
        int n = resMatrix.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n);
            int value = resMatrix[k];
            resMatrix[k] = resMatrix[n];
            resMatrix[n] = value;
        }

        //note: rn this will leave some objects with the default value of 14 (and thus undrawn)
        //since i didn't add the junk to json yet
        GameObject[] numbers = GameObject.FindGameObjectsWithTag("Number");
        for (int i = 0; i < resMatrix.Count; i++)
        {
            numbers[i].GetComponent<Number>().value = resMatrix[i];
            numbers[i].GetComponent<Number>().Draw();
        }
    }
}
