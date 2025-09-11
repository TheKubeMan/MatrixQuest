using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public enum Colorr
    {
        red, green, blue,
        yellow, orange, white,
        purple, cyan
    }
    public Colorr color;
    public Color32 value;

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
        //when there will be a model for the key this 
        //will need to be changed to set the needed part's color, not the entire model's
        gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor", value);
    }
}
