using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Looking : MonoBehaviour
{

	public Transform orientation;
	public float sensetivity;
	float mouseX, mouseY;
	float x, y;
	float xR, yR;
	public Wallrun wallrun;
	InputManager input;

    // Start is called before the first frame update
    void Start()
    {
		sensetivity = PlayerPrefs.GetInt("Sensetivity", 100) * 3;
		input = new InputManager();
		input.Player.Enable();
		input.Player.Camera.performed += ctx => x = ctx.ReadValue<Vector2>().x;
		input.Player.Camera.performed += ctx => y = ctx.ReadValue<Vector2>().y;
		input.Player.Camera.canceled += ctx => x = 0;
		input.Player.Camera.canceled += ctx => y = 0;
        Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
		mouseX = x * sensetivity * Time.deltaTime;
		mouseY = y * sensetivity * Time.deltaTime;
		xR -= mouseY;
		xR = Mathf.Clamp(xR, -90, 90);
		yR += mouseX;

		transform.localRotation = Quaternion.Euler(xR, yR, wallrun.tilt);
		orientation.transform.rotation = Quaternion.Euler(0, yR, 0);
    }
}
