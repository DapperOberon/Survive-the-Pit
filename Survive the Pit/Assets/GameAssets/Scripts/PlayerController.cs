using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

	Vector2 mouselook;
	Vector2 smoothV;

	[Header("Mouse Settings")]
	public float mouseSensitivity = 5f;
	public bool invertMouse = false;

	[Header("Controller Settings")]
	public float controllerSensitivity = 2.5f;
	public bool invertController = false;

	[Header("General Camera Settings")]
	public float smoothing = 1f;
	public float viewClampUp = -90f;
	public float viewClampDown = 80f;

	[Header("Player Movement")]
	public float moveSpeed = 2f;
	public float strafeSpeed = 1f;
	public float sprintRate = 2f;
	public float crouchRate = 0.5f;
	public float jumpSpeed = 1f;
	public float gravity = 9.8f;
	public bool toggleSprint = false;
	private static bool isSprinting = false;
	public bool toggleCrouch = false;
	private bool isCrouching = false;

	private Vector3 moveDir = Vector3.zero;
	
	private GameObject player;
	private CharacterController playerCC;
	private GameObject mainCam;

	private void Awake()
	{
		player = this.gameObject;
		playerCC = gameObject.GetComponent<CharacterController>();
		mainCam = Camera.main.gameObject;

		InputManager.instance.HideCursor();
	}

	private void Update()
	{
		RotateCamera();
		MovePlayer();
	}

	private void RotateCamera()
	{
		if(InputManager.instance.GetInputState() == InputManager.eInputState.MouseKeyboard)
		{
			CreateMouselookV(mouseSensitivity);

			if (invertMouse)
			{
				mainCam.transform.localRotation = Quaternion.AngleAxis(-mouselook.y, Vector3.right);
			}
			else
			{
				mainCam.transform.localRotation = Quaternion.AngleAxis(mouselook.y, Vector3.right);
			}
		}
		else if(InputManager.instance.GetInputState() == InputManager.eInputState.Controller)
		{
			CreateMouselookV(controllerSensitivity);

			if (invertController)
			{
				mainCam.transform.localRotation = Quaternion.AngleAxis(-mouselook.y, Vector3.right);
			}
			else
			{
				mainCam.transform.localRotation = Quaternion.AngleAxis(mouselook.y, Vector3.right);
			}
		}
			
		player.transform.localRotation = Quaternion.AngleAxis(mouselook.x, player.transform.up);
	}

	private void CreateMouselookV(float sensitivity)
	{
		Vector2 md = new Vector2(hInput.GetAxis("RotateCameraX"), hInput.GetAxis("RotateCameraY"));

		md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
		smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
		smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
		mouselook += smoothV;
		mouselook.y = Mathf.Clamp(mouselook.y, viewClampUp, viewClampDown);
	}

	private void MovePlayer()
	{
		if (playerCC.isGrounded)
		{
			float translation = hInput.GetAxis("Move") * moveSpeed;
			float strafe = hInput.GetAxis("Strafe") * strafeSpeed;
			moveDir = new Vector3(strafe, 0, translation);

			Sprint();
			Crouch();

			moveDir = transform.TransformDirection(moveDir);

			if (hInput.GetButtonDown("Jump"))
			{
				moveDir.y = jumpSpeed;
			}
		}
		moveDir.y -= gravity * Time.deltaTime;
		playerCC.Move(moveDir * Time.deltaTime);
	}

	private void Sprint()
	{
		if(Player.GetStamina() > 0)
		{
			if (toggleSprint)
			{
				if (hInput.GetButtonDown("Sprint"))
				{
					isSprinting = !isSprinting;

				}

				if (isSprinting)
				{
					moveDir.z *= sprintRate;
				}
			}
			else
			{
				if (hInput.GetButton("Sprint"))
				{
					moveDir.z *= sprintRate;
					isSprinting = true;
				}
				else
				{
					isSprinting = false;
				}
			}
		}
	}

	public static bool IsSprinting()
	{
		return isSprinting;
	}

	private void Crouch()
	{
		if (toggleCrouch)
		{
			if (hInput.GetButtonDown("Crouch"))
			{
				isCrouching = !isCrouching;

			}

			if (isCrouching)
			{
				moveDir.z *= crouchRate;
				playerCC.height = 1.2f;
			}
			else
			{
				Ray ray = new Ray();
				RaycastHit hit;
				ray.origin = transform.position;
				ray.direction = Vector3.up;
				if (!Physics.Raycast(ray, out hit, 1))
				{
					playerCC.height = 1.8f;
				}
				else
				{
					Debug.Log("Not enough space to stand up!");
				}
			}
		}
		else
		{
			if (hInput.GetButton("Crouch"))
			{
				moveDir.z *= crouchRate;
				playerCC.height = 1.2f;
			}
			else
			{
				Ray ray = new Ray();
				RaycastHit hit;
				ray.origin = transform.position;
				ray.direction = Vector3.up;
				if (!Physics.Raycast(ray, out hit, 1))
				{
					playerCC.height = 1.8f;
					Debug.Log("Nothing above, standing up...");
				}
				else
				{
					Debug.Log("Not enough space to stand up!");
				}
			}
		}
	}
}
