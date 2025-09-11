using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class KeyReader : MonoBehaviour
{
    public enum Colorr
    {
        red, green, blue,
        yellow, orange, white,
        purple, cyan
    }
    public Colorr color;
    public int requiredKeys;
    Color32 value;
    bool usedAKey = false;
    public UnityEvent action;
    // Start is called before the first frame update
    void Start()
    {
        switch (color)
        {
            case Colorr.red:
                value = new Color32 (178, 31, 31, 255);
                break;
            case Colorr.green:
                value = new Color32 (15, 153, 24, 255);
                break;
            case Colorr.blue:
                value = new Color32 (52, 83, 221, 255);
                break;
            case Colorr.yellow:
                value = new Color32 (222, 200, 13, 255);
                break;
            case Colorr.white:
                value = new Color32 (255, 255, 255, 255);
                break;
            case Colorr.orange:
                value = new Color32 (196, 108, 13, 255);
                break;
            case Colorr.purple:
                value = new Color32 (161, 10, 198, 255);
                break;
            case Colorr.cyan:
                value = new Color32 (26, 186, 207, 255);
                break;
        }
    }

    public void Interaction(GameObject player)
    {
        //Change the Debug.Log to a ui message displayed for the player
        if (requiredKeys == 0)
            Debug.Log("key requirements are already met");
        else
        {
            usedAKey = false;
            for (int i = player.GetComponent<PlayerInventory>().keys.Count - 1; i >= 0; i--)
                if (requiredKeys > 0 && value == player.GetComponent<PlayerInventory>().keys[i])
                {
                    requiredKeys--;
                    usedAKey = true;
                    player.GetComponent<PlayerInventory>().keys.RemoveAt(i);
                    player.GetComponent<PlayerInventory>().DrawInventory();
                    if (requiredKeys == 0)
                        action.Invoke();
                    break;
                }
            if (requiredKeys > 0 && !usedAKey)
                //change into a ui message as well
                Debug.Log("you don't have the right key...");
        }
    }
}
