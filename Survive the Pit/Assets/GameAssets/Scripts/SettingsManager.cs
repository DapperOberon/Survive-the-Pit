using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour {

	public AudioMixer mixer;

	public void SetMasterVolume(float volume)
	{
		mixer.SetFloat("MasterVolume", volume);
	}

	public void SetBGMVolume(float volume)
	{
		mixer.SetFloat("BGMVolume", volume);
	}
}
