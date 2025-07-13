using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public Transform orientation;
    public LayerMask interactable;
    public float interactionDistance;
    RaycastHit ray;
    //public GameObject pauseMenu;
	bool paused = false;
	public InputManager input;
    // Start is called before the first frame update
    void Start()
    {
        input = new InputManager();
		input.Player.Enable();
    }

    // Physics related stuff must be in FixedUpdate
    void FixedUpdate()
    {
        if (Physics.Raycast(orientation.position, Camera.main.transform.forward, out ray, interactionDistance, interactable))
        {
            Debug.Log("Object with type " + ray.transform.gameObject.GetComponent<Item>().type + " in sight");
            //display a prompt for interaction based on the type
            //for example, if it's a number, then it's a pickup icon and text
            //or, if it's a button, then it's "press the button" and so on

            if (input.Player.Interact.WasPressedThisFrame() && !paused)
                ray.transform.gameObject.GetComponent<Item>().Interaction();
        }
        else
            Debug.Log("");  
    }
}
