using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public Transform orientation;
    public LayerMask interactable;
    public float interactionDistance;
    RaycastHit ray;
    //child[0] - icon
    //child[1] - text
    public GameObject prompt;
    public Sprite PickUp, Press, InsertKey;
    bool canInteract, paused = false;
    public InputManager input;
    
    void Start()
    {
        input = new InputManager();
        input.Player.Enable();
        input.Player.Interact.performed += ctx => Interact();
    }

    // Physics related stuff must be in FixedUpdate
    void FixedUpdate()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out ray, interactionDistance, interactable))
        {
            prompt.SetActive(true);
            switch (ray.transform.GetComponent<Item>().pType)
            {
                case "number":
                    prompt.transform.GetChild(1).GetComponent<Text>().text = "Подобрать";
                    prompt.transform.GetChild(0).GetComponent<Image>().sprite = PickUp;
                    break;
                case "button":
                    prompt.transform.GetChild(1).GetComponent<Text>().text = "Нажать";
                    prompt.transform.GetChild(0).GetComponent<Image>().sprite = Press;
                    break;
                case "key":
                    prompt.transform.GetChild(1).GetComponent<Text>().text = "Подобрать";
                    prompt.transform.GetChild(0).GetComponent<Image>().sprite = PickUp;
                    break;
                case "keyReader":
                    prompt.transform.GetChild(1).GetComponent<Text>().text = "Использовать";
                    prompt.transform.GetChild(0).GetComponent<Image>().sprite = InsertKey;
                    break;
            }
            //change the text of the prompt according to the object's type
            canInteract = true;
        }
        else
        {
            prompt.SetActive(false);
            canInteract = false;
        }
    }

    void Interact()
    {
        if (!paused && canInteract)
            ray.transform.gameObject.GetComponent<Item>().Interaction(gameObject);
    }

    //visualising the interaction ray
    //not really needed, but leaving it just in case
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * interactionDistance);
    }
}
