using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using DFTGames.Localization;

public class SettingsManager : MonoBehaviour {

	public GameObject[] menus;
	public Slider[] sliders;
	public AudioMixer mixer;

	public TMP_Dropdown resDropdown;
	private Resolution[] resolutions;

	public TMP_Dropdown qualityDropdown;

	private void Start()
	{
		GetResolutions();
		LoadPrefs();
	}

	private void LoadPrefs()
	{
		mixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume"));
		sliders[0].value = PlayerPrefs.GetFloat("MasterVolume");

		mixer.SetFloat("BGMVolume", PlayerPrefs.GetFloat("BGMVolume"));
		sliders[1].value = PlayerPrefs.GetFloat("BGMVolume");

		QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Quality"));
		qualityDropdown.value = PlayerPrefs.GetInt("Quality");
		print(PlayerPrefs.GetInt("Quality"));
	}

	private void GetResolutions()
	{
		resolutions = Screen.resolutions;

		resDropdown.ClearOptions();

		List<string> options = new List<string>();

		int currentResolutionIndex = 0;

		for (int i = 0; i < resolutions.Length; i++)
		{
			string option = resolutions[i].width + " x " + resolutions[i].height;
			options.Add(option);

			if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
			{
				currentResolutionIndex = i;
			}
		}

		resDropdown.AddOptions(options);
		resDropdown.value = currentResolutionIndex;
		resDropdown.RefreshShownValue();
	}

	public void SetMasterVolume(float volume)
	{
		mixer.SetFloat("MasterVolume", volume);
		PlayerPrefs.SetFloat("MasterVolume", volume);
	}

	public void SetBGMVolume(float volume)
	{
		mixer.SetFloat("BGMVolume", volume);
		PlayerPrefs.SetFloat("BGMVolume", volume);
	}

	public void SetQuality(int quality)
	{
		QualitySettings.SetQualityLevel(quality);
		PlayerPrefs.SetInt("Quality", quality);
	}

	public void SetFullscreen(bool isFullscreen)
	{
		Screen.fullScreen = isFullscreen;
		PlayerPrefs.SetString("Fullscreen", isFullscreen.ToString());
	}

	public void SetResolution(int resIndex)
	{
		Resolution resolution = resolutions[resIndex];
		Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
	}

	public void SetEnglish()
	{
		EnableMenus();

		LocalizeTM.SetCurrentLanguage(SystemLanguage.English);
		LocalizeImage.SetCurrentLanguage();
	}

	public void SetGerman()
	{
		EnableMenus();

		LocalizeTM.SetCurrentLanguage(SystemLanguage.German);
		LocalizeImage.SetCurrentLanguage();
	}

	private void EnableMenus()
	{
		foreach(GameObject go in menus)
		{
			go.SetActive(true);
		}
	}

	public void DisableMenus()
	{
		foreach(GameObject go in menus)
		{
			go.SetActive(false);
		}
	}

	public void SavePrefs()
	{
		PlayerPrefs.Save();
	}
}
