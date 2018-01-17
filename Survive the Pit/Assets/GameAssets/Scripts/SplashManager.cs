using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashManager : MonoBehaviour {

	bool isReady = false;
	public Animator inputPromptAnim;

	IEnumerator Start()
	{
		yield return new WaitForSeconds(2); // TODO Add functionality
		isReady = true;
	}

	private void Update()
	{
		if (isReady)
		{
			inputPromptAnim.SetBool("isReady", true);
			if (Input.anyKeyDown)
			{
				SceneManager.LoadScene(1); // TODO talk to the Scene Manager gameobject
			}
		}
	}
}
