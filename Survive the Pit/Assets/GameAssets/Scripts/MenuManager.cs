using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	public Button continueBtn;
	private bool doesSaveExist = false;

	public void SetSelectedButton(GameObject go)
	{
		EventSystem ES = EventSystem.current;
		ES.SetSelectedGameObject(go, new BaseEventData(ES));
	}

	public void Continue()
	{
		// TODO Add rich loading functionality
		Debug.Log("Loading game...");
	}

	public void NewGame()
	{
		// TODO Add rich new game functionality
		Debug.Log("Creating new game...");
	}

	public void Quit()
	{
		Debug.Log("Quitting...");
		Application.Quit();
	}

	private void OnGUI()
	{
		// TODO Add rich file checking functionality
		if (doesSaveExist)
		{
			continueBtn.interactable = true;
		}
		else
		{
			continueBtn.interactable = false;
		}
	}
}
