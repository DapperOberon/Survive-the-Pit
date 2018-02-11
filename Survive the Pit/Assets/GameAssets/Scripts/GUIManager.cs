using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUIManager : MonoBehaviour {

	public static GUIManager instance;

	// Player Stats
	public Slider healthSlider, hungerSlider, thirstSlider, staminaSlider;

	// Time and Date
	public TMP_Text timeDateText, extTemp, temp;

	// Screens
	public GameObject deathScreen;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		timeDateText.text = TimeManager.instance.toString(); // TODO Possibly add static instance and call toString in an updateText function in TimeManager
		extTemp.text = TimeManager.instance.temperatureToString();
		healthSlider.value = Player.GetHealth() / 100;
		hungerSlider.value = Player.GetHunger() / 100;
		thirstSlider.value = Player.GetThirst() / 100;
		staminaSlider.value = Player.GetStamina() / 100;
	}
}
