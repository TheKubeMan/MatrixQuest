using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool singlePlayer = false;
    
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        //needed to read the problems from a json file
        StreamReader reader = new StreamReader("problems.json");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
