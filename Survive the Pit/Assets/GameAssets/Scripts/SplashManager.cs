using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SplashManager : MonoBehaviour {

	bool isReady = false;
	public Animator inputPromptAnim;

	public TextMeshProUGUI mText;

	IEnumerator Start()
	{
		yield return new WaitForSeconds(2); // TODO Add functionality
		isReady = true;
	}

	private void Update()
	{
		if (isReady)
		{
			inputPromptAnim.SetBool("isReady", true);
			if (hInput.GetButtonDown("Select"))
			{
				SceneManager.LoadScene(1); // TODO talk to the Scene Manager gameobject
			}
		}
	}

	private void OnGUI()
	{
		switch (InputManager.instance.GetInputState())
		{
			case InputManager.eInputState.MouseKeyboard:
				mText.text = "Press Enter to start...";
				break;
			case InputManager.eInputState.Controller:
				mText.text = "Press A to start...";
				break;
		}
	}
}
