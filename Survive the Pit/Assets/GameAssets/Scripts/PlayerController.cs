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
	public float jumpSpeed = 1f;
	public float gravity = 9.8f;

	private Vector3 moveDir = Vector3.zero;
	
	private GameObject player;
	private CharacterController playerCC;
	private GameObject mainCam;

	private void Start()
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

	// TODO Add sprinting
	private void MovePlayer()
	{
		if (playerCC.isGrounded)
		{
			float translation = hInput.GetAxis("Move") * moveSpeed;
			float strafe = hInput.GetAxis("Strafe") * strafeSpeed;
			moveDir = new Vector3(strafe, 0, translation);
			moveDir = transform.TransformDirection(moveDir);
			if (hInput.GetButtonDown("Jump"))
			{
				moveDir.y = jumpSpeed;
			}
		}
		moveDir.y -= gravity * Time.deltaTime;
		playerCC.Move(moveDir * Time.deltaTime);
	}
}
