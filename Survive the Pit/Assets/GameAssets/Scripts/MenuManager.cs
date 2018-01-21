using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour {

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
}
