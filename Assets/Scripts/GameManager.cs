using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class GameManager : MonoBehaviour
{
    public bool singlePlayer = false;
    public static GameManager GM;
    public static Task currentTask;
    public Root allData;
    Dictionary<string, Task> taskMap;

    //classes for reading from JSON
    [System.Serializable]
    public class Task
    {
        public int[][] a;
        public int[][] b;
        public int[][] answer;
    }
    [System.Serializable]
    public class Size
    {
        public Task task0;
        public Task task1;
        public Task task2;
    }
    [System.Serializable]
    public class Root
    {
        public Size _2x2;
        public Size _2x3;
        public Size _3x3;
        public Size _3x4;
        public Size _4x4;
    }

    void Awake()
    {
        if (GM == null)
        {
            DontDestroyOnLoad(this);
            GM = this;
        }
        else if (GM != this)
            Destroy(this);

        //needed to read the problems from a json file
        string filePath = Application.dataPath + "/tasks.json";
        string jsonString = File.ReadAllText(filePath);
        //allData refuses to be filled with the JSON data, even tho the string is ok
        //i honestly don't know how to fix it and what to do
        allData = JsonConvert.DeserializeObject<Root>(jsonString);
        Debug.Log(allData._2x2.task0.a[0][0]);
        taskMap = new Dictionary<string, Task>
        {
            { "0_0", allData._2x2.task0 },
            { "0_1", allData._2x2.task1 },
            { "0_2", allData._2x2.task2 },
            { "1_0", allData._2x3.task0 },
            { "1_1", allData._2x3.task1 },
            { "1_2", allData._2x3.task2 },
            { "2_0", allData._3x3.task0 },
            { "2_1", allData._3x3.task1 },
            { "2_2", allData._3x3.task2 },
            { "3_0", allData._3x4.task0 },
            { "3_1", allData._3x4.task1 },
            { "3_2", allData._3x4.task2 },
            { "4_0", allData._4x4.task0 },
            { "4_1", allData._4x4.task1 },
            { "4_2", allData._4x4.task2 }
        };
    }

    public void SingleGame(int level)
    {
        singlePlayer = true;
        //int variant = Random.Range(0, 3);
        //for testing, variant will always be 0
        int variant = 0;
        //get data from the jsonString based on the variant value
        string key = $"{level}_{variant}";
        taskMap.TryGetValue(key, out currentTask);

        string levelName = "Single" + level + "_" + variant;
        //yet again, just for testing, the SampleScene will be loaded instead of levelName
        SceneManager.LoadScene("SampleScene");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
