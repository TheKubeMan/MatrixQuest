using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public Transform orientation;
    public LayerMask interactable;
    public float interactionDistance;
    RaycastHit ray;
    //public GameObject pauseMenu;
    bool canInteract, paused = false;
    public InputManager input;
    // Start is called before the first frame update
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
            //display a prompt for interaction based on the type
            //for example, if it's a number, then it's a pickup icon and text
            //or, if it's a button, then it's "press the button" and so on
            canInteract = true;
        }
        else
        {
            //disable the promt and maybe clean up the values, idk yet
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
