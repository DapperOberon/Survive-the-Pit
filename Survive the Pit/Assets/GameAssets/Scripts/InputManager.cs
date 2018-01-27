using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	public static InputManager instance = null;

	private bool isCursorVisible = false;

	private void Awake()
	{
		// Create Singleton patter
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if(instance != this)
		{
			Destroy(gameObject);
		}

		/* Assign each keycode when the game starts
		 * Load data from PlayerPrefs
		 * Default values are set via the second parameter of GetString()
		 */
	}


	public enum eInputState
	{
		MouseKeyboard,
		Controller
	};

	private eInputState m_State = eInputState.MouseKeyboard;

	//*************************//
	// Unity member methods    //
	//*************************//

	void OnGUI()
	{
		switch (m_State)
		{
			case eInputState.MouseKeyboard:
				if (isControllerInput())
				{
					m_State = eInputState.Controller;
					Debug.Log("InputManager - Controller being used");
					HideCursor();
				}
				break;
			case eInputState.Controller:
				if (isMouseKeyboard())
				{
					m_State = eInputState.MouseKeyboard;
					Debug.Log("InputManager - Mouse & Keyboard being used");
					ShowCursor();
				}
				break;
		}
	}

	//***************************//
	// Public member methods     //
	//***************************//

	public eInputState GetInputState()
	{
		return m_State;
	}

	//****************************//
	// Private member methods     //
	//****************************//

	private void ShowCursor()
	{
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}

	private void HideCursor()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	private bool isMouseKeyboard()
	{
		// mouse & keyboard buttons
		if (Event.current.isKey ||
			Event.current.isMouse)
		{
			return true;
		}
		// mouse movement
		if (Input.GetAxis("Mouse X") != 0.0f ||
			Input.GetAxis("Mouse Y") != 0.0f)
		{
			return true;
		}
		return false;
	}

	private bool isControllerInput()
	{
		// joystick buttons
		if (Input.GetKey(KeyCode.Joystick1Button0) ||
		   Input.GetKey(KeyCode.Joystick1Button1) ||
		   Input.GetKey(KeyCode.Joystick1Button2) ||
		   Input.GetKey(KeyCode.Joystick1Button3) ||
		   Input.GetKey(KeyCode.Joystick1Button4) ||
		   Input.GetKey(KeyCode.Joystick1Button5) ||
		   Input.GetKey(KeyCode.Joystick1Button6) ||
		   Input.GetKey(KeyCode.Joystick1Button7) ||
		   Input.GetKey(KeyCode.Joystick1Button8) ||
		   Input.GetKey(KeyCode.Joystick1Button9) ||
		   Input.GetKey(KeyCode.Joystick1Button10) ||
		   Input.GetKey(KeyCode.Joystick1Button11) ||
		   Input.GetKey(KeyCode.Joystick1Button12) ||
		   Input.GetKey(KeyCode.Joystick1Button13) ||
		   Input.GetKey(KeyCode.Joystick1Button14) ||
		   Input.GetKey(KeyCode.Joystick1Button15) ||
		   Input.GetKey(KeyCode.Joystick1Button16) ||
		   Input.GetKey(KeyCode.Joystick1Button17) ||
		   Input.GetKey(KeyCode.Joystick1Button18) ||
		   Input.GetKey(KeyCode.Joystick1Button19))
		{
			return true;
		}

		// joystick axis
		if (Input.GetAxis("Controller Axis-All-Axis1") != 0.0f ||
		   Input.GetAxis("Controller Axis-All-Axis2") != 0.0f ||
		   Input.GetAxis("Controller Axis-All-Axis4") != 0.0f ||
		   Input.GetAxis("Controller Axis-All-Axis5") != 0.0f ||
		   Input.GetAxis("Controller Axis-All-Axis9") != 0.0f ||
		   Input.GetAxis("Controller Axis-All-Axis10") != 0.0f)
		{
			return true;
		}

		return false;
	}
}
