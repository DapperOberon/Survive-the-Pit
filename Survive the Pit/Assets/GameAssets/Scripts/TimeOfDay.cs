using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeOfDay : MonoBehaviour {

	[SerializeField] private Light sun;

	private float sunInitialIntensity;

	private void Start()
	{
		sunInitialIntensity = sun.intensity;
	}

	private void Update()
	{
		UpdateSun();
	}

	private void UpdateSun()
	{
		sun.transform.localRotation = Quaternion.Euler((TimeManager.instance.totalSeconds / (TimeManager.instance.dayLengthInSeconds / 360)) - 90, 0, 0);

		float intensityMultiplier = 1;

		// TODO Rework sun logic for use with TimeManager
		//// Sun intensity logic
		//if (TimeManager.instance.totalSeconds <= 0.23f || TimeManager.instance.totalSeconds >= 0.75f)
		//{
		//	intensityMultiplier = 0;
		//}
		//else if (TimeManager.instance.totalSeconds <= 0.25f)
		//{
		//	intensityMultiplier = Mathf.Clamp01((TimeManager.instance.totalSeconds - 0.23f) * (1 / 0.02f));
		//}
		//else if (TimeManager.instance.totalSeconds >= 0.73f)
		//{
		//	intensityMultiplier = Mathf.Clamp01(1 - ((TimeManager.instance.totalSeconds - 0.73f) * (1 / 0.02f)));
		//}

		sun.intensity = sunInitialIntensity * intensityMultiplier;
	}
}
