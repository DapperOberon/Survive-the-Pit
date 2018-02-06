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
		float time = TimeManager.instance.totalSeconds;
		float dayLengh = TimeManager.instance.dayLengthInSeconds;
		float intensityMultiplier = 1;
		float time0To1 = Mathf.InverseLerp(0, dayLengh, time);

		sun.transform.localRotation = Quaternion.Euler((time / (dayLengh / 360)) - 90, 0, 0);

		// TODO Change time0to1 to work with standard seconds
		// TODO Fix ambient lighting issue
		// Sun intensity logic
		if (time0To1 <= 0.23f || time0To1 >= 0.75f)
		{
			intensityMultiplier = 0;
			//DynamicGI.UpdateEnvironment();
		}
		else if (time0To1 <= 0.25f)
		{
			intensityMultiplier = Mathf.Clamp01((time0To1 - 0.23f) * (1 / 0.02f));
			//DynamicGI.UpdateEnvironment();
		}
		else if (time0To1 >= 0.73f)
		{
			intensityMultiplier = Mathf.Clamp01(1 - ((time0To1 - 0.73f) * (1 / 0.02f)));
			//DynamicGI.UpdateEnvironment();
		}

		sun.intensity = sunInitialIntensity * intensityMultiplier;
		//RenderSettings.ambientIntensity = intensityMultiplier;
	}
}
