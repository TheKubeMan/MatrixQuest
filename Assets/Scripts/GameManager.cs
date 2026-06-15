using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public static class SaveSystem
{
    public static void SaveResults(int size)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/personalResults" + size + ".savedTime";
        FileStream stream = new FileStream(path, FileMode.Create);
        
        List<int[]> data = null;
        switch (size)
        {
            case 0:
                data = GameManager.Record2x2;
                break;
            case 1:
                data = GameManager.Record2x3;
                break;
            case 2:
                data = GameManager.Record3x3;
                break;
            case 3:
                data = GameManager.Record3x4;
                break;
            case 4:
                data = GameManager.Record4x4;
                break;
        }
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static List<int[]> LoadResults(int size)
    {
        string path = Application.persistentDataPath + "/personalResults" + size + ".savedTime";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            List<int[]> data = formatter.Deserialize(stream) as List<int[]>;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}

public class GameManager : MonoBehaviour
{
    public bool singlePlayer = false;
    public static GameManager GM;
    public static Task currentTask;
    public static int currentSize;
    public static List<int[]> Record2x2, Record2x3, Record3x3, Record3x4, Record4x4;
    public GameObject table, rowPrefab;
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
            if (SceneManager.GetActiveScene().name != "MainMenu")
                Destroy(this);

        SaveSystem.LoadResults(0);
        SaveSystem.LoadResults(1);
        SaveSystem.LoadResults(2);
        SaveSystem.LoadResults(3);
        SaveSystem.LoadResults(4);

        //needed to read the problems from a json file
        try
        {
            string filePath = Application.streamingAssetsPath + "/tasks.json";
            string jsonString = File.ReadAllText(filePath);
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
        catch
        {
            Debug.Log("There should be a messagebox saying that you have a problem with the json here, but i'm lazy");
        }
    }

    public void SingleGame(int level)
    {
        singlePlayer = true;
        int variant = Random.Range(0, 2);
        //get data from the jsonString based on the variant value
        string key = $"{level}_{variant}";
        currentSize = level;
        taskMap.TryGetValue(key, out currentTask);

        string levelName = "Single" + level + "_" + variant;
        SceneManager.LoadScene(levelName);
    }

    public void DrawLeaderboard(int size)
    {
        foreach (Transform child in table.transform)
            Destroy(child.gameObject);

        List<int[]> data = null;
        switch (size)
        {
            case 0:
                data = GameManager.Record2x2;
                break;
            case 1:
                data = GameManager.Record2x3;
                break;
            case 2:
                data = GameManager.Record3x3;
                break;
            case 3:
                data = GameManager.Record3x4;
                break;
            case 4:
                data = GameManager.Record4x4;
                break;
        }

        data.Sort((a, b) => a[1].CompareTo(b[1]));

        for (int i = 0; i < data.Count; i++)
        {
            int finalTime = data[i][1];

            int miliseconds = finalTime % 100;
            int seconds = (finalTime / 100) % 60;
            int minutes = finalTime / 6000;

            string time = minutes + ":" + seconds + ":" + miliseconds; 

            GameObject row = Instantiate(rowPrefab, table.transform);
            row.transform.GetChild(0).gameObject.GetComponent<Text>().text = (i + 1).ToString();
            row.transform.GetChild(1).gameObject.GetComponent<Text>().text = time;
            row.transform.GetChild(2).gameObject.GetComponent<Text>().text = data[i][0].ToString();
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
