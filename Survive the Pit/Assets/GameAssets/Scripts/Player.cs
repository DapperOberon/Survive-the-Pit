using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public float maxHealth, maxHunger, maxThirst, maxStamina, maxTemp, minTemp;
	private static float health, hunger, thirst, stamina, temp;
	public float healthRate, hungerRate, thirstRate, staminaRate, calorieRate;
	private const float normalTemp = 98.6f;

	private bool dead;

	// Use this for initialization
	void Start () {
		health = maxHealth;
		healthRate = 1000; // roughly 1 day
		hunger = maxHunger;
		hungerRate = 21000; // roughly 21 days
		thirst = maxThirst;
		thirstRate = 7000; // roughly 7 days
		stamina = maxStamina;
		staminaRate = 8; // 8 seconds
		temp = normalTemp;
	}
	
	// Update is called once per frame
	void Update () {
		// Health
		if(health <= 0)
		{
			Die();
		}

		// Hunger
		if(hunger > 0)
		{
			if (PlayerController.IsSprinting())
			{
				hunger -= (Time.deltaTime / (hungerRate / calorieRate)) * TimeManager.TIMESCALE;
			}
			else
			{
				hunger -= (Time.deltaTime / hungerRate) * TimeManager.TIMESCALE;
			}
			
		}
		else if(hunger <= 0)
		{
			health -= (Time.deltaTime / healthRate) * TimeManager.TIMESCALE;
		}

		// Thirst
		if(thirst > 0)
		{
			if (PlayerController.IsSprinting())
			{
				thirst -= (Time.deltaTime / (thirstRate / calorieRate)) * TimeManager.TIMESCALE;
			}
			else
			{
				thirst -= (Time.deltaTime / thirstRate) * TimeManager.TIMESCALE;
			}
			
		}
		else if(thirst <= 0)
		{
			health -= (Time.deltaTime / healthRate) * TimeManager.TIMESCALE;
		}

		// Stamina
		if (PlayerController.IsSprinting())
		{
			if (stamina > 0)
			{
				stamina -= Time.deltaTime * staminaRate;
			}
		}
		else if(stamina < maxStamina)
		{
			stamina += Time.deltaTime * staminaRate;
		}
		
		// Temp
		CheckTemp();
	}

	private void Die()
	{
		Time.timeScale = 0;
		GetComponent<PlayerController>().enabled = false;
		GUIManager.instance.deathScreen.SetActive(true);
	}

	private void CheckTemp()
	{
		// TODO Add weather system to get current environment temp from
	}

	public static float GetHealth()
	{
		return health;
	}

	public static float GetHunger()
	{
		return hunger;
	}

	public static float GetThirst()
	{
		return thirst;
	}

	public static float GetStamina()
	{
		return stamina;
	}

	public static float GetTemp()
	{
		return temp;
	}
}
