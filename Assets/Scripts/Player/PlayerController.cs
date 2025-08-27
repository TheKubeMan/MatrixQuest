using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{
	float moveX;
	float moveZ;
	public float speed = 90;
	public bool slopeSlide = false;
	bool startSlowingDown = false;
	public float airSpeed = 0.5f;
	public float jumpPower = 0.5f;
	public float groundDrag;
	public float airDrag;
	public float slideGM = 0.5f;
	float slideSM = 200;
	public bool sliding;
	public float slideStartSpeed;
	public bool grounded;
	public float groundCheckRadius;
	public LayerMask ground, ceiling;
	public Transform orientation;
	public Transform groundCheck, ceilingCheck;
	public bool somethingAbove = false;
	Vector3 movement;
	RaycastHit slopeHit;
	Vector3 slopeMovement;
	bool slideblock = false;
	bool canIgnore = false;
	public bool canSlide = true;
	bool jumped = false;
	Rigidbody rb;
	public GameObject pauseMenu;
	bool paused = false;

	public InputManager input;
    // Start is called before the first frame update
    void Start()
    {
		input = new InputManager();
		input.Player.Enable();
		input.Player.Jump.performed += context => Jump();

		input.Player.Pause.performed += context => Pause();
		input.Pause.Unpause.performed += ContextMenu => Unpause();

		input.Player.Movement.performed += ctx => moveX = ctx.ReadValue<Vector2>().x;
		input.Player.Movement.performed += ctx => moveZ = ctx.ReadValue<Vector2>().y;
		input.Player.Movement.canceled += ctx => moveX = 0;
		input.Player.Movement.canceled += ctx => moveZ = 0;

		rb = gameObject.GetComponent<Rigidbody>();
		rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;

        grounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, ground);

		//save momentum after sliding down a slope
		if (sliding && rb.velocity.magnitude > 13)
			slopeSlide = true;
		if (rb.velocity.magnitude < 6 || !grounded)
			slopeSlide = false;
		if (slopeSlide && !OnSlope() && !startSlowingDown)
		{
			slideGM = 0.8f;
			startSlowingDown = true;
		}
		else if (startSlowingDown)
			slideGM -= 0.0018f;
		if (slideGM <= 0.5f || rb.velocity.magnitude <= 6.2f)
		{
			slopeSlide = false;
			startSlowingDown = false;
			slideGM = 0.5f;
		}

		//the bool for preventing flying off the flat surface instead of moving down a slope
		if (!grounded && rb.velocity.y > 2f)
			jumped = true;

		if (grounded && rb.velocity.y <= 0)
			canSlide = true;

		if ((grounded || canIgnore) && input.Player.Slide.IsPressed())
		{
			if (canSlide)
				if ((rb.velocity.magnitude > slideStartSpeed && sliding == false) || OnSlope())
				{
					sliding = true;
					gameObject.transform.localScale = new Vector3(1, 0.5f, 1);
					if (!OnSlope())
						gameObject.transform.position = new Vector3(transform.position.x, transform.position.y - .5f, transform.position.z);
					else
					{
						canIgnore = true;
						rb.AddForce(Vector3.down * 10, ForceMode.Force);
					}
				}
		}
		else
			ExitSlide();

		InputHandler();
		ControlDrag();

		slopeMovement = Vector3.ProjectOnPlane(movement, slopeHit.normal);
    }

	// Place anything physics related here!
	void FixedUpdate()
	{
		MovePlayer();
	}

	bool OnSlope()
	{
		if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, 3f))
		{
			if (slopeHit.normal != Vector3.up)
				return true;
			else
				return false;
		}
		return false;
	}

	void ControlDrag()
	{
		if (grounded)
		{
			rb.drag = groundDrag;
			jumped = false;
		}
		else
			rb.drag = airDrag;
	}

	void InputHandler()
	{
		movement = orientation.transform.forward * moveZ + orientation.transform.right * moveX;
	}

	void MovePlayer()
	{
		if (grounded && !OnSlope())
		{
			if (!sliding)
				rb.AddForce(movement.normalized * speed, ForceMode.Acceleration);
			else
			{
				if (rb.velocity.magnitude > 6.2f)
					rb.AddForce(movement.normalized * speed * slideGM, ForceMode.Acceleration);
				rb.AddForce(-rb.velocity.normalized * Time.deltaTime * 0.00001f, ForceMode.Acceleration);
			}
		}
		else if (grounded && OnSlope())
		{
			if (slideblock)
				slideSM = 600;
			else
				slideSM = 200;
			rb.AddForce(slopeMovement.normalized * speed, ForceMode.Acceleration);
			if (sliding)
			{
				rb.AddForce(Vector3.down * slideSM, ForceMode.Acceleration);
				if (rb.velocity.magnitude < 0.01f)
					slideblock = true;
			}
			else
				slideblock = false;
			//this adds a downward force to the player when he's not moving 
			//to prevent flying off the slope's edge 
			if (moveX > 0 || moveZ > 0)
				rb.AddForce(-transform.up * 7.5f, ForceMode.Acceleration);
		}
		else if (!grounded)
			rb.AddForce(movement.normalized * speed * airSpeed, ForceMode.Acceleration);
			//this prevents flying off from a flat surface if there's a slope under the player
			if (OnSlope() && !jumped)
				rb.AddForce(-transform.up * 2000 * Time.deltaTime, ForceMode.Acceleration);
	}

	void Jump()
	{
		if (grounded)
		{
			canSlide = false;
			sliding = false;
			canIgnore = false;
			gameObject.transform.localScale = new Vector3(1, 1, 1);
			rb.velocity = new Vector3 (rb.velocity.x, 0, rb.velocity.z);
			rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
		}
	}
	
	void Pause()
	{
		pauseMenu.SetActive(true);
		pauseMenu.GetComponent<PauseMenu>().Pause();
		input.Player.Disable();
		input.Pause.Enable();
		paused = true;
	}
	public void Unpause()
	{
		pauseMenu.GetComponent<PauseMenu>().Unpause();
		pauseMenu.SetActive(false);
		input.Pause.Disable();
		input.Player.Enable();
		paused = false;
	}
	void ExitSlide()
	{
		somethingAbove = Physics.CheckSphere(ceilingCheck.position, 0.25f, ceiling);
		if (!somethingAbove)
		{
			sliding = false;
			canIgnore = false;
			gameObject.transform.localScale = new Vector3(1, 1, 1);
		}
		else
		{
			gameObject.transform.localScale = new Vector3(1, 0.4f, 1);
			if (!paused)
				rb.AddForce(orientation.forward * 10, ForceMode.Acceleration);
		}
	}
}