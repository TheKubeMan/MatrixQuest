using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    bool finished = false;
    int miliseconds = 0;
    int seconds = 0;
    int minutes = 0;
    string TimerString;
    public int finalTime = 0;
    public GameObject TimeText;
    void FixedUpdate()
    {
        if (finished == false)
        {
            miliseconds += 2;
            if (miliseconds == 100){
                seconds++;
                miliseconds = 0;
            }
            if (seconds == 60){
                minutes++;
                seconds = 0;
            }
            if(miliseconds < 10 && seconds >= 10){
            TimerString = minutes + ":" + seconds + ":0" + miliseconds;
            }
            if(miliseconds < 10 && seconds < 10){
                TimerString = minutes + ":0" + seconds + ":0" + miliseconds;
            }
            if(miliseconds >= 10 && seconds < 10){
                TimerString = minutes + ":0" + seconds + ":" + miliseconds;
            }
            if(miliseconds >= 10 && seconds >= 10){
                TimerString = minutes + ":" + seconds + ":" + miliseconds;
            }
        }
        TimeText.GetComponent<TextMeshProUGUI>().text = TimerString;
    }

    public void Finish()
    {
        finished = true;
        finalTime = (minutes * 60 * 100) + (seconds * 100) + miliseconds;
    }
}
