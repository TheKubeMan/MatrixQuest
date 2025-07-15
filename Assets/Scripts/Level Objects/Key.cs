using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public enum Colorr
    {
        red, green, blue,
        yellow, white, black
    }
    public Colorr color;
    public Color value;

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
        //when there will be a model for the key this 
        //will need to be changed to set the needed part's color, not the entire model's
        gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor", value);
    }
}
