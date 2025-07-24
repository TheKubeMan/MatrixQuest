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
        yellow, white, black
    }
    public Colorr color;
    public int requiredKeys;
    Color value;
    public UnityEvent action;
    // Start is called before the first frame update
    void Start()
    {
        //temp colors, will be changed to have 16 (or at least 8) presets that look good
        switch (color)
        {
            case Colorr.red:
                value = Color.red;
                break;
            case Colorr.green:
                value = Color.green;
                break;
            case Colorr.blue:
                value = Color.blue;
                break;
            case Colorr.yellow:
                value = Color.yellow;
                break;
            case Colorr.white:
                value = Color.white;
                break;
            case Colorr.black:
                value = Color.black;
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
            for (int i = player.GetComponent<PlayerInventory>().keys.Count - 1; i >= 0; i--)
                if (requiredKeys > 0 && value == player.GetComponent<PlayerInventory>().keys[i])
                {
                    requiredKeys--;
                    player.GetComponent<PlayerInventory>().keys.RemoveAt(i);
                    if (requiredKeys == 0)
                        action.Invoke();
                    break;
                }
            if (requiredKeys > 0)
                Debug.Log("you don't have the right key...");
        }
    }
}
