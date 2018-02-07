using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour {

	public static TimeManager instance = null;
	public static int TIMESCALE = 1; // Use this to modify the day speed

	// TIME //
	[Tooltip("Time of day in seconds")]
	public float time;
	[Tooltip("Day length in seconds")]
	public float dayLength = 86400;

	// TimeOfDay
	[SerializeField] private Light sun;

	private float sunInitialIntensity;

	[Header("Phase Start Times")]
	public int dawnStartTime = 6;
	public int dayStartTime = 8;
	public int duskStartTime = 18;
	public int nightStartTime = 20;

	public float sunDimTime; // Sun dim speed
	public float dawnSunIntensity = 0.5f;
	public float daySunIntensity = 0.1f;
	public float duskSunIntensity = 0.25f;
	public float nightSunIntensity = 0f;

	public DayPhases dayPhases;
	public enum DayPhases
	{
		Dawn,
		Day,
		Dusk,
		Night
	}

	IEnumerator TimeOfDay()
	{
		while (true)
		{
			switch (dayPhases)
			{
				case DayPhases.Dawn:
					Dawn();
					break;
				case DayPhases.Day:
					Day();
					break;
				case DayPhases.Dusk:
					Dusk();
					break;
				case DayPhases.Night:
					Night();
					break;
			}
			yield return null;
		}
	}

	private void Dawn()
	{
		Debug.Log("Dawn");

		if(sun.intensity < dawnSunIntensity) // If sun is not dawSunIntensity, then go up to it
		{
			sun.intensity += Time.deltaTime * (sunDimTime * TIMESCALE); // Increase sun intensity by sunDimTime
		}

		// Change to Day phase
		if (getHour() >= dayStartTime && getHour() < duskStartTime)
		{
			dayPhases = DayPhases.Day;
		}
	}

	private void Day()
	{
		Debug.Log("Day");

		// Change to Dusk phase
		if (getHour() >= duskStartTime && getHour() < nightStartTime)
		{
			dayPhases = DayPhases.Dusk;
		}
	}

	private void Dusk()
	{
		Debug.Log("Dusk");

		// Change to Night phase
		if (getHour() >= nightStartTime)
		{
			dayPhases = DayPhases.Night;
		}
	}

	private void Night()
	{
		Debug.Log("Night");

		// Change to Dawn phase
		if (getHour() >= dawnStartTime && getHour() < dayStartTime)
		{
			dayPhases = DayPhases.Dawn;
		}
	}

	// DATE //
	public Dictionary<int, string> monthDict = new Dictionary<int, string>
	{
		{ 1, "January" },
		{ 2, "February" },
		{ 3, "March" },
		{ 4, "April" },
		{ 5, "May" },
		{ 6, "June" },
		{ 7, "July" },
		{ 8, "August" },
		{ 9, "September" },
		{ 10, "October" },
		{ 11, "November" },
		{ 12, "December" }
	};
	public Dictionary<int, string> seasonDict = new Dictionary<int, string>
	{
		{ 1, "Spring" },
		{ 2, "Summer" },
		{ 3, "Fall" },
		{ 4, "Winter" }
	};

	private int day, month, season, year;

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

		//time = (int)getCurrentDateTime().TimeOfDay.TotalSeconds; // TODO Make rich loading time
		day = getCurrentDateTime().Day;
		month = getCurrentDateTime().Month;
		year = getCurrentDateTime().Year;
		CalculateSeason();

		// 
		//sunInitialIntensity = sun.intensity;
		sun.intensity = nightSunIntensity;
		dayPhases = DayPhases.Night;
	}

	private void Start()
	{
		StartCoroutine(TimeOfDay());
		// Set day phase based on time
	}
	private void Update () {
		CalculateDateTime();
		UpdateSun();

	}

	private void CalculateDateTime()
	{
		time += Time.deltaTime * TIMESCALE;

		if(time >= dayLength)
		{
			day++;
			time = 0;
		}
		else if(day >= 28)
		{
			CalculateMonth();
		}
	}

	public DateTime getCurrentDateTime()
	{
		DateTime dateTime = DateTime.Now;
		return dateTime;
	}

	// Time methods
	#region
	private int getHour()
	{
		return (int)time / 60 / 60;
	}

	private int getMinute()
	{
		return (int)time / 60 % 60;
	}

	private int getSecond()
	{
		return (int)time % 60;
	}
	#endregion

	// Date methods
	#region
	private int getDay()
	{
		return day;
	}
	
	private string getMonth()
	{
		return monthDict[month];
	}

	private string getSeason()
	{
		return seasonDict[season];
	}

	private int getYear()
	{
		return year;
	}

	private void CalculateMonth()
	{
		// Months with 31 days
		if(month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10)
		{
			// Set on day more than 31
			if(day >= 32)
			{
				month++;
				day = 1;
			}
		}
		// February
		else if (month == 2)
		{
			// Leap Year
			if (IsLeapYear())
			{
				// Set one day more than 29
				if (day >= 30)
				{
					month++;
					day = 1;
				}
			}
			// Common year
			else
			{
				// Set one more day than 28
				if (day >= 29)
				{
					month++;
					day = 1;
				}
			}
		}
		// Months with 30 days
		else if (month == 4 || month == 6 || month == 9 || month == 11)
		{
			// Set one day more than 30
			if(day >= 31)
			{
				month++;
				day = 1;
			}
		}
		// December
		else if(month == 12)
		{
			// Set one day more than 31
			if(day >= 32)
			{
				year++;
				month = 1;
				day = 1;
			}
		}

		// Calculate the season based on the month
		CalculateSeason();
	}

	private void CalculateSeason()
	{
		// Spring ~ March 1 to May 31
		if ((month >= 3 && day >= 1) && (month <= 5 && day <= 31))
		{
			season = 1;
		}
		// Summer ~ June 1 to August 31
		else if ((month >= 6 && day >= 1) && (month <= 8 && day <= 31))
		{
			season = 2;
		}
		// Fall ~ September 1 to November 30
		else if ((month >= 9 && day >= 1) && (month <= 11 && day <= 30))
		{
			season = 3;
		}
		// Winter
		else
		{
			season = 4;
		}
	}

	private bool IsLeapYear()
	{
		if(year % 4 == 0 || year % 100 == 0 && year % 400 == 0)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	#endregion

	public string toUniversalString()
	{
		return string.Format("{0:D2}:{1:D2}:{2:D2}\n{3} {4:D}, {5:D4}", getHour(), getMinute(), getSecond(), getMonth(), getDay(), getYear());
	}

	public string toString()
	{
		return string.Format("{0:D}:{1:D2}:{2:D2} {3}\n{4} {5:D}, {6:D4}", ((getHour() == 0 || getHour() == 12) ? 12 : getHour() % 12), getMinute(), getSecond(), (getHour() < 12 ? "AM" : "PM"), getMonth(), getDay(), getYear());
	}

	private void UpdateSun()
	{
		sun.transform.localRotation = Quaternion.Euler((time / (dayLength / 360)) - 90, 0, 0);

		//float intensityMultiplier = 1;
		//float time0To1 = Mathf.InverseLerp(0, dayLength, time);

		

		//// TODO Change time0to1 to work with standard seconds
		//// TODO Fix ambient lighting issue
		//// Sun intensity logic
		//if (time0To1 <= 0.23f || time0To1 >= 0.75f)
		//{
		//	intensityMultiplier = 0;
		//	//DynamicGI.UpdateEnvironment();
		//}
		//else if (time0To1 <= 0.25f)
		//{
		//	intensityMultiplier = Mathf.Clamp01((time0To1 - 0.23f) * (1 / 0.02f));
		//	//DynamicGI.UpdateEnvironment();
		//}
		//else if (time0To1 >= 0.73f)
		//{
		//	intensityMultiplier = Mathf.Clamp01(1 - ((time0To1 - 0.73f) * (1 / 0.02f)));
		//	//DynamicGI.UpdateEnvironment();
		//}

		//sun.intensity = sunInitialIntensity * intensityMultiplier;
		////RenderSettings.ambientIntensity = intensityMultiplier;
	}
}
