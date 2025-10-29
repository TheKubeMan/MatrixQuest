using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Number : MonoBehaviour
{
    public int value;
    [Header("Meshes")]
    public GameObject num1;
    public GameObject num2, num3, num4, num5, num6, num7, num8, num9, num0;
    [Header("Values")]
    public float numWidth;
    public float gap;
    // Start is called before the first frame update
    void Start()
    {
        string numstr = value.ToString();
        int count = 0;
        foreach (char i in numstr)
        {
            switch (i)
            {
                case '0':
                    Instantiate(num0, transform);
                    break;
                case '1':
                    Instantiate(num1, transform);
                    break;
                case '2':
                    Instantiate(num2, transform);
                    break;
                case '3':
                    Instantiate(num3, transform);
                    break;
                case '4':
                    Instantiate(num4, transform);
                    break;
                case '5':
                    Instantiate(num5, transform);
                    break;
                case '6':
                    Instantiate(num6, transform);
                    break;
                case '7':
                    Instantiate(num7, transform);
                    break;
                case '8':
                    Instantiate(num8, transform);
                    break;
                case '9':
                    Instantiate(num9, transform);
                    break;
            }
            count++;
        }
        float pos0 = (-(numWidth * count + gap * (count - 1)) / 2) + (numWidth / 2);
        for (int i = 0; i < count; i++)
            transform.GetChild(i).localPosition = new Vector3(pos0 + (i * (gap + numWidth)), 0, 0);
        gameObject.GetComponent<BoxCollider>().size = new Vector3(numWidth * count + gap * (count - 1), 2, 2);
    }
}
