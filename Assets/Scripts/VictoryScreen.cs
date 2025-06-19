using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class VictoryScreen : MonoBehaviour
{
    int maxComboo;
    int ennemies;
    int time;
    int total;
    public GameObject TimeScoreText, ComboScoreText, EnemyScoreText, RankText, selectedButton;
    
    public void End()
    {
        time = Camera.main.GetComponent<Timer>().finalTime;
        total = (maxComboo * 100) + ((60000 - time) * 200) + (ennemies * 50);
        TimeScoreText.GetComponent<TextMeshProUGUI>().text = ((60000 - time) * 100).ToString();
        ComboScoreText.GetComponent<TextMeshProUGUI>().text = (maxComboo * 100).ToString();
        EnemyScoreText.GetComponent<TextMeshProUGUI>().text = (ennemies * 50).ToString();
        EventSystem.current.SetSelectedGameObject(selectedButton);
        if (total <= 15000)
        {
            RankText.GetComponent<TextMeshProUGUI>().text = "E";
        }
        else if (total > 15000 && total < 30000)
        {
            RankText.GetComponent<TextMeshProUGUI>().text = "D";
        }
        else if (total > 15000 && total <= 30000)
        {
            RankText.GetComponent<TextMeshProUGUI>().text = "C";
        }
        else if (total > 30000 && total <= 45000)
        {
            RankText.GetComponent<TextMeshProUGUI>().text = "B";
        }
        else if (total > 45000 && total <= 60000)
        {
            RankText.GetComponent<TextMeshProUGUI>().text = "A";
        }
        else if (total > 60000)
        {
            RankText.GetComponent<TextMeshProUGUI>().text = "S";
        }
    }
}
