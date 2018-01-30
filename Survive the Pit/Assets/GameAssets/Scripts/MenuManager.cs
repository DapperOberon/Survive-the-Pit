using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour {

	public static MenuManager instance = null;

	// Arrays
	public GameObject[] menus;
	public Slider[] sliders;

	// Buttons
	public Button continueBtn;
	public Button newGameBtn;
	public bool doesSaveExist = true; // TODO Add rich check and make private

	// Graphics
	public TMP_Dropdown resDropdown;
	public TMP_Dropdown qualityDropdown;

	// TODO move to SceneManager
	public int gameIndex = 2;

	private void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		else if(instance != this)
		{
			Destroy(gameObject);
		}
	}

	public void SetSelectedButton(GameObject go)
	{
		EventSystem ES = EventSystem.current;
		ES.SetSelectedGameObject(go, new BaseEventData(ES));
	}

	public void Continue()
	{
		// TODO Add rich loading functionality
		Debug.Log("Loading game...");
		SceneManager.LoadSceneAsync(2); // TODO Move to SceneManager
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

	private void Start()
	{
		// TODO Add rich file checking functionality
		if (doesSaveExist)
		{
			continueBtn.interactable = true;
		}
		else
		{
			continueBtn.interactable = false;
			SetSelectedButton(newGameBtn.gameObject);
		}
	}
}
